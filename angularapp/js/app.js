var app = angular.module('myApp',['ngResource','ngRoute']);

//Configure routes , interceptor etc.
app.config(function($routeProvider){
    $routeProvider.when('/',{
        templateUrl: 'view/home.html',
        controller: 'HomeCtrl'
    })
    .when('/WeatherInfo',{
        templateUrl: 'view/WeatherInfo.html',
        controller: 'WeatherInfoCtrl'
    });
});

app.config(function($httpProvider){
    $httpProvider.interceptors.push('myInterceptor');
});

app.factory('myInterceptor',function($log){
    return{
        request: function(config){
            console.log('Intercepting request...');
            //we can show loading-progress icon here...
            return config;
        },
        response: function(config){
            console.log('Intercepting response...');
            //we can hide loading-progress icon here 
            return config;
        },
        requestError: function(config){
            console.log('Intercepting error...');
            return config;
        }
    }
});

app.controller('MainCtrl',function($scope,LoggerSvc){
    $scope.testMessage = "MainCtrl test message.";  
});

app.controller('HomeCtrl',function($scope ){
    $scope.homeTestMessage = "Home controller test message";
});



