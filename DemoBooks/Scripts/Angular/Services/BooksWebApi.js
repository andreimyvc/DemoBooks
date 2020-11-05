(function () {
    'use strict';
    var app = angular.module('BooksWebApi', []);

    app.factory('booksDataFactory', ['$http', '$q', BooksDataFactory]);
    function BooksDataFactory(http, q) {
        http.defaults.headers.common['__RequestVerificationToken'] = $('[name=__RequestVerificationToken]').val();
        var interfaz = {};


        interfaz.getAll = function () {
            var defer = q.defer();
            http.get('/api/books/').then(function (response) {
                defer.resolve(response.data);
            }, function (response) {
                defer.reject(response);
            });
            return defer.promise;
        }
        interfaz.getById = function (id) {
            var defer = q.defer();
            http.get('/api/books/', { params: { id: id } }).then(function (response) {
                defer.resolve(response.data);
            }, function (response) {
                defer.reject(response);
            });
            return defer.promise;
        }

        interfaz.insertBook = function (entity) {
            var defer = q.defer();
            http({ method: 'POST', url: '/api/books', data: entity }).then(function (response) {
                defer.resolve(response.data);
            }, function (response) {
                defer.reject(response);
            });
            return defer.promise;
        };
        
        interfaz.updateBook = function (entity) {
            var defer = q.defer();
            http({ method: 'PUT', url: '/api/books/put', data: entity }).then(function (response) {
                defer.resolve(response.data);
            }, function (response) {
                defer.reject(response);
            });
            return defer.promise;
        };
        interfaz.deleteBook = function (id) {
            var defer = q.defer();
            http.delete('/api/books/', { params: { id: id } }).then(function (response) {
                defer.resolve(response.data);
            }, function (response) {
                defer.reject(response);
            });
            return defer.promise;
        };
        return interfaz;
    }
})();