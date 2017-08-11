(function () {
    'use strict';
    var controllerId = 'films';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', '$uibModal', films]);

    function films(common, datacontext, $scope, $uibModal) {
        var getLogFn = common.logger.getLogFn;
        var logError = getLogFn(controllerId, 'logError');
        var vm = this;

        $scope.createOrEditFilm = function(film) {
            var modalInstance = $uibModal.open({
                controller: 'filmEditor',
                templateUrl: 'app/films/filmEditor.html',
                resolve: {
                    film: function () {
                        return film;
                    }
                }
            });

            modalInstance.result.then(function (film) {
                    getFilms();
            }, function () { });
        }
        
        $scope.ratingClick = function (film) {
            updateFilm(film);
        }

        $scope.gridOptions = {
            columnDefs: [
                {
                    field: 'Title', displayName: 'Title', width: 175,
                    cellTemplate: '<div class="ui-grid-cell-contents"><a ng-click="grid.appScope.createOrEditFilm(row.entity)" style="cursor:pointer;">{{row.entity.Title}}</a></div>'
                },
                { field: 'Director', displayName: 'Director'},
                {
                    field: 'ReleaseDate', displayName: 'Release Date',
                    cellFilter: 'date:"MMM dd, yyyy"'
                },
                {
                    field: 'Rating', displayName: 'Rating', width: 100,
                    cellTemplate: '<div class="ui-grid-cell-contents"><uib-rating ng-model="row.entity.Rating" max="5" ng-click="grid.appScope.ratingClick(row.entity)"></uib-rating></div>'
                }
            ],
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
            }
        };

        activate();

        function updateFilm(film) {
            return datacontext.updateFilm(film).then(function (films) { }, function (response) {
                logError(response, this, false);
            });
        }
        
        function activate() {
            var promises = [getFilms()];
            common.activateController(promises, controllerId)
                .then(function () { });
        }

        function getFilms() {
            return datacontext.getFilms().then(function (films) {
                $scope.gridOptions.data = films;
            }, function (response) {
                logError(response, this, false);
            });
        }
    }
})();