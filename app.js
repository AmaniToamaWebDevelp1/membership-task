(function(){
    'use strict';
     
    angular.module('myFirstApp',[])
    .controller('mycontroller', function($scope){
        $scope.name = "";
        $scope.totalValue = 0;
        // $scope.lastName = "Toama";
        // $scope.sayHello =  function() {
        //     return `Hello ${$scope.name} ${$scope.lastName}`;
        // };
        $scope.totalValue = 0;

        $scope.displayNumeric = function(){
           var totalNumValue = CalculateNumFORString($scope.name);
            
            $scope.totalValue = totalNumValue;
        };
        function CalculateNumFORString(string){
            var totaleStringValue = 0;
            for(var i = 0; i < string.length; i++){
                totaleStringValue += string.charCodeAt(i);
            }
            return totaleStringValue;
        }

    });
   
})();