var MittoAgSmsApp = angular.module('MittoAgSmsApp',
["kendo.directives"]);

MittoAgSmsApp.constant("appConfig",
{
    "apiUrl": "http://localhost:2919"
});


MittoAgSmsApp.filter("jsDate", function () {
    return function (x) {
        return new Date(parseInt(x.substr(6)));
    };
});

MittoAgSmsApp.factory('dataService', function ($http, appConfig) {

    var factory = {};
    var apiUrl = appConfig.apiUrl;

    factory.GetCountries = function () {
        return $http.post(apiUrl + '/api/countries');
    };

    factory.GetSentSMS = function (model) {
        return $http.post(apiUrl + '/api/sms/sent', model);
    }

    factory.SendSMS = function (model) {
        return $http.post(apiUrl + '/api/sms/send', model);
    }

    factory.GetStatistics = function (model) {
        return $http.post(apiUrl + '/api/statistics', model);
    }

    return factory;
});


MittoAgSmsApp.controller('inboxCtrl',
function ($scope, dataService) {

    $scope.resetSearchFormModel = function () {
        $scope.searchFormModel = {
            fromDate: kendo.toString(new Date(), "dd.MM.yyyy"),
            toDate: kendo.toString(new Date(), "dd.MM.yyyy"),
            take: 10,
            skip: 0
        };

        $scope.items = [];
        $scope.totalCount = 0;
        $scope.hasMoreRecords = false;
        $scope.counts = [10, 20, 50, 100, 1000];
    };

    $scope.resetSearchFormModel();
    
    $scope.onBtnSearchClick = function (resetSearch) {
        if (resetSearch) {
            $scope.items = [];
        };

        var postData = {
            dateTimeFrom: kendo.toString(kendo.parseDate($scope.searchFormModel.fromDate), "yyyy-MM-dd"),
            dateTimeTo: kendo.toString(kendo.parseDate($scope.searchFormModel.toDate), "yyyy-MM-dd"),
            take: $scope.searchFormModel.take,
            skip: $scope.searchFormModel.skip
        };

        dataService.GetSentSMS(postData).success(function (data) {
            $scope.totalCount = data.totalCount;
            angular.forEach(data.items, function (item) {
                $scope.items.push(item);
            });
            $scope.hasMoreRecords = $scope.totalCount > ($scope.searchFormModel.skip + $scope.searchFormModel.take);
        });
    };

    $scope.onBtnSearchClick(true);

    $scope.loadMoreRecords = function () {
        $scope.searchFormModel.skip = $scope.searchFormModel.skip + $scope.searchFormModel.take;
        $scope.onBtnSearchClick(false);
    };

    $scope.isBtnSearchDisabled = function () {
        return false;
    };

    $scope.onBtnResetClick = function () {
        $scope.resetSearchFormModel();
    };

    $scope.onPageSizeChanged = function () {
        $scope.onBtnSearchClick(true);
    };

    $scope.$on('on-sms-send', function (e, data) {
        $scope.onBtnSearchClick(true);
    });

});

MittoAgSmsApp.controller('sendSmsCtrl',
function ($rootScope, $scope, dataService) {

    $scope.sendSmsFormModel = {
        from: '',
        to: '',
        text: ''
    };

    $scope.smsSucesfullySend = false;
    $scope.errorWhileSending = false;

    $scope.onBtnSendClick = function() {
        $scope.errorWhileSending = false;
        $scope.smsSucesfullySend = false;
        dataService.SendSMS($scope.sendSmsFormModel)
            .success(function(data) {

                $rootScope.$broadcast('on-sms-send', $scope.sendSmsFormModel);

                $scope.sendSmsFormModel.from = '';
                $scope.sendSmsFormModel.to = '';
                $scope.sendSmsFormModel.text = '';

                $scope.smsSucesfullySend = true;
            })
            .error(function() {
                $scope.errorWhileSending = true;
            });
    };

    $scope.isFormModelValid = function () {
        if ($scope.sendSmsFormModel.from.length == 0)
            return false;
        if ($scope.sendSmsFormModel.to.length == 0)
            return false;
        if ($scope.sendSmsFormModel.text.length == 0)
            return false;
        return true;
    };
});


MittoAgSmsApp.controller('statsCtrl',
    function($rootScope, $scope, dataService) {

        dataService.GetCountries()
            .success(function (data) {
                $scope.countrySelectOptions = {
                    placeholder: "Select countries...",
                    dataTextField: "Name",
                    dataValueField: "Mcc",
                    valuePrimitive: true,
                    autoBind: true,
                    itemTemplate: '#: data.Name # (#: data.Mcc #)',
                    tagTemplate: '#: data.Name # (#: data.Mcc #)',
                    dataSource: data
                };
            });

        $scope.resetSearchFormModel = function() {
            $scope.searchFormModel = {
                dateFrom: kendo.toString(new Date(), "dd.MM.yyyy"),
                dateTo: kendo.toString(new Date(), "dd.MM.yyyy"),
                mccList: []
            };
            $scope.items = [];
        };

        $scope.resetSearchFormModel();

        $scope.onBtnSearchClick = function() {
            $scope.items = [];

            var postData = {
                dateFrom: kendo.toString(kendo.parseDate($scope.searchFormModel.dateFrom), "yyyy-MM-dd"),
                dateTo: kendo.toString(kendo.parseDate($scope.searchFormModel.dateTo), "yyyy-MM-dd"),
                mccList: $scope.searchFormModel.mccList
            };

            dataService.GetStatistics(postData)
                .success(function(data) {
                    $scope.items = data;
                });
        };

        $scope.onBtnSearchClick();

        $scope.isBtnSearchDisabled = function() {
            return false;
        };

        $scope.onBtnResetClick = function() {
            $scope.resetSearchFormModel();
        };
    });




MittoAgSmsApp.controller('countryListCtrl',
    function ($scope, dataService) {

        $scope.items = [];
        dataService.GetCountries()
            .success(function (data) {
                $scope.items = data;
            });
    });