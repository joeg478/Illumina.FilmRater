(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, ['$http', 'common', datacontext]);

    function datacontext($http, common) {
        var getLogFn = common.logger.getLogFn;
        var logError = getLogFn(serviceId, 'logError');

        var service = {
            getFilms: getFilms,
            saveFilm: saveFilm,
            updateFilm: updateFilm
        };

        return service;

        function getFilms() {
            var url = 'api/Film';
            return $http({
                method: 'GET',
                url: url
            }).then(function(response) {
                return response.data;
                }, function (response) {
                    logError("Error getting films", response, false);
                return response;
            });
        }

        function updateFilm(film) {
            var url = 'api/Film';
            return $http({
                method: 'PUT',
                url: url,
                data: film
            }).then(function (response) {
                return response.data;
                }, function (response) {
                    logError("Error updating film", response, false);
                    return response;
            });
        }

        function saveFilm(film) {
            var url = 'api/Film';
            return $http({
                method: 'POST',
                url: url,
                data:film
            }).then(function (response) {
                return response.data;
                }, function (response) {
                    logError("Error saving film", response, false);
                    return response;
            });
        }
    }
})();