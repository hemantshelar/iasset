var app = angular.module('myApp');

app.factory('LoggerSvc',['$log',function($log){
    function messageWithTime(message){
        var time = new Date();
        return time.toString() + ' : ' + message;
    }
    return{
        log: function(message){
            $log.log(messageWithTime(message));
        },
        warn: function(message){
            $log.warn(messageWithTime(message));
        },
        info: function(message){
            $log.info(messageWithTime(message));
        },
        error: function(message){
            $log.error(messageWithTime(message));
        },
        debug: function(message){
            $log.debug(messageWithTime(message));
        }
    }
}]);
