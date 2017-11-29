app = angular.module('myApp');
app.controller('WeatherInfoCtrl',['$scope','WheatherResourceSvc','LoggerSvc',function($scope,WheatherResourceSvc,LoggerSvc){
    $scope.countries = [];
    $scope.cities = [];
    $scope.weatherInfo = null;
    $scope.selectedCountry = "";
    $scope.selectedCity = "";

    $scope.onCountryChanged = function(){
        WheatherResourceSvc.getCities({country: $scope.selectedCountry.Name},function(data){
            $scope.cities = data;
        });
    }

    $scope.onCityChanged  = function(){
        debugger;
        WheatherResourceSvc.getWeatherInfo({country: $scope.selectedCity.Country,city: $scope.selectedCity.Name},
        function(data){
            $scope.weatherInfo = data;
        },function(e){
            alert('No data found for this city.');
            LoggerSvc.warn('No data found for city : ' + $scope.selectedCity.Name);
            $scope.weatherInfo = null;
        });
    }

    //Init
    WheatherResourceSvc.getCountries(function(data){
        $scope.countries = data;
    });
}]);