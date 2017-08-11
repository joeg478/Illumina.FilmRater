(function () {
    'use strict';
    var controllerId = 'filmEditor';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$scope', '$uibModalInstance', 'film', filmEditor]);

    function filmEditor(common, datacontext, $scope, $uibModalInstance, film) {
        var getLogFn = common.logger.getLogFn;
        var logError = getLogFn(controllerId, 'logError');
        var vm = this; 

        $scope.viewTitle = function () {
            if ($scope.film.Title == null)
                return "New Film";
            else
                return $scope.film.Title;
        }

        $scope.ok = function () {
            if ($scope.filmEditorFrm.$valid) {
                if ($scope.film.Id == null) {
                    return datacontext.saveFilm($scope.film).then(function (film) {
                        $uibModalInstance.close(film);
                    });
                }
                else {
                    return datacontext.updateFilm($scope.film).then(function (film) {
                        $uibModalInstance.close(film);
                    });
                }
            }
            else {
                angular.forEach($scope.filmEditorFrm.$error, function (field) {
                    angular.forEach(field, function (errorField) {
                        errorField.$setTouched();
                    })
                    logError('Film editor is invalid', null, false);
                });
                alert("Form is invalid.");
            }
        }

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function init() {
            if (film != null)
                $scope.film = JSON.parse(JSON.stringify(film));
            else
                $scope.film = {};

            if ($scope.film.ReleaseDate != null)
                $scope.film.ReleaseDate = moment($scope.film.ReleaseDate).toDate();
        }

        activate();

        function activate() {
            init();
            var promises = [];
            common.activateController(promises, controllerId)
                .then(function () { });
        }

        $scope.today = function () {
            $scope.film.ReleaseDate = new Date();
        };

        $scope.clear = function () {
            $scope.film.ReleaseDate  = null;
        };
        
        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(1800, 1, 1),
            startingDay: 1
        };
        
        $scope.openReleaseDatePicker = function () {
            $scope.releaseDatePicker.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.film.ReleaseDate = new Date(year, month, day);
        };

        $scope.formats = ['MMM dd, yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];

        $scope.altInputFormats = ['mm/dd/yyyy'];

        $scope.releaseDatePicker = {
            opened: false
        };
    }
})();