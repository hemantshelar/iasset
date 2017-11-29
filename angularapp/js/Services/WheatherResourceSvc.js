var app = angular.module('myApp');

app.factory('WheatherResourceSvc',function($resource){
    var baseApiUrl = "http://publicapitest.azurewebsites.net/";
    //var baseApiUrl = "http://localhost:92/"
    return $resource( baseApiUrl + 'api/WeatherInfo/:id',{id: "@id"},
        { 
            getCountries : {method: 'GET', url: baseApiUrl + 'api/WeatherInfo/getCountries' , params: {} , isArray: true} ,
            getCities : {method: 'GET', url: baseApiUrl + 'api/WeatherInfo/getCities' , params: {} , isArray: true} ,
            getWeatherInfo : {method: 'GET', url: baseApiUrl + 'api/WeatherInfo/getWeatherInfo' , params: {} , isArray: false}
        }
    );    
});