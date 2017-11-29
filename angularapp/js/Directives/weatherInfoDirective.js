var app = angular.module('myApp');

app.directive('weatherInfo',function(){
    return{
        restricts: 'E',
        templateUrl: 'view/weatherInfoDirective.html'
        //ideally I should create isolated scope here...
    }
})