//"use strict";

var booksApp = angular.module('booksApp', ['BooksWebApi']);
var booksController = function (scope, http, booksDataFactory) {
    var d = scope;
    d.control =
    {
        fieldsReadOnly: true,
        state: 'view',
        btns:
        {
            Nuevo: { show: true },
            Editar: { show: false },
            Eliminar: { show: false },
            Cancelar: { show: false },
            Guardar: { show: false }
        }
    };
    d.filter = null;
    //#region SetVariables

    //#endregion SetVariables
    //#region botones
    d.clearFilter = function () {
        d.filter = null;
    }
    d.clearFields = function () {
        d.model = {};
    };
    d.newEntrada = function () {
        d.model = {};
        d.control.fieldsReadOnly = false;
        d.control.btns.Nuevo.show = false;
        d.control.btns.Editar.show = false;
        d.control.btns.Guardar.show = true;
        d.control.btns.Cancelar.show = true;
        d.control.btns.Eliminar.show = false;
        d.control.state = 'add';
    };
    d.cancelEntrada = function () {
        d.control.state = 'view'
        d.control.fieldsReadOnly = true;
        d.control.btns.Nuevo.show = true;
        d.control.btns.Cancelar.show = false;
        d.control.btns.Guardar.show = false;
        d.clearFields();
        d.model = null;
    };
    d.editEntrada = function () {
        d.control.state = 'edit'
        d.control.fieldsReadOnly = false;
        d.control.btns.Nuevo.show = false;
        d.control.btns.Cancelar.show = true;
        d.control.btns.Editar.show = false;
        d.control.btns.Guardar.show = true;
    };
    d.saveEntrada = function () {
        if (!d.model) {
            xAlerta("¡Todos los campos son requeridos!");
            return;
        }
        if (!d.model.Title) {
            xAlerta("¡Debe indicar el título!");
            return;
        }
        if (!d.model.Description) {
            xAlerta("¡Debe indicar la descripción!");
            return;
        }
        if (!d.model.Excerpt) {
            xAlerta("¡Debe indicar el campo Excerpt!");
            return;
        }
        if (!(d.model.PageCount > 0)) {
            xAlerta("¡Debe indicar la cantidad de Páginas!");
            return;
        }
        if (!d.model.PublishDate) {
            xAlerta("¡Debe indicar la fecha de Publicación!");
            return;
        }
        d.control.fieldsReadOnly = true;
        d.control.btns.Nuevo.show = true;
        d.control.btns.Cancelar.show = false;
        d.control.btns.Guardar.show = false;
        if (d.control.state === 'add') {
            booksDataFactory.insertBook(d.model)
                .then(function (data) {
                    d.model.ID = data.ID;
                    /*Insertamos el registro en el listado para simular el insert */
                    d.booksData.unshift(d.model);
                    //getData();
                    d.control.state = 'view';
                    xAlerta("OK!");
                    d.clearFields();
                    d.model = null;
                    $('#vistaDetail').modal('hide');
                }, function (error) {
                    d.control.fieldsReadOnly = false;
                    d.control.btns.Nuevo.show = false;
                    d.control.btns.Cancelar.show = true;
                    d.control.btns.Guardar.show = true;
                    d.error = error.data.Message;
                    xAlerta(d.error);
                });
        }
        else {
            booksDataFactory.updateBook(d.model).then(function (data) {
                xAlerta("OK");
                var old = d.booksData.find(item => { return item.ID == d.model.ID; });
                /*Reemplazamos los datos para simular el update */
                old.ID = d.model.ID;
                old.Title = d.model.Title;
                old.Description = d.model.Title;
                old.Excerpt = d.model.Excerpt;
                old.PublishDate = d.model.PublishDate;
                old.PageCount = d.model.PageCount;
                //getData();
                d.model = null;
                $('#vistaDetail').modal('hide');
            }, function (error) {
                d.error = error.data.Message;
                xAlerta(d.error);
                d.control.fieldsReadOnly = false;
                d.control.btns.Nuevo.show = false;
                d.control.btns.Cancelar.show = true;
                d.control.btns.Guardar.show = true;
            });
        }
    };

    d.deleteEntrada = function (id) {
        var callBack = function () {
            booksDataFactory.deleteBook(id - 0).then(
                function (result) {
                    d.control.state = 'view';
                    d.control.fieldsReadOnly = true;
                    d.control.btns.Nuevo.show = true;
                    d.control.btns.Cancelar.show = false;
                    d.control.btns.Guardar.show = false;
                    d.control.btns.Eliminar.show = false;
                    d.control.btns.Editar.show = false;
                    d.clearFields();
                    getData();
                    xAlerta("Registro eliminado.");
                }, function (error) {
                    d.error = error.data.Message;
                    xAlerta(d.error);
                });
        }
        xConfirm("¿Está seguro que desea eliminar este registro?", callBack);       
    };
    //#endregion Botones
    //#region GETs
    function getData() {
        booksDataFactory.getAll().then(
            function (data) {
                d.booksData = data;
            }, function (error) {
                d.error = error.data.Message;
                bootbox.alert(d.error);
            });
    };
    d.setSelectedEntity = function (id) {
        d.model = null;

        var t = d.booksData.find(function (item) { return item.ID == id; });
        d.model = {};

        d.model.ID = t.ID;
        d.model.Title = t.Title;
        d.model.Description = t.Title;
        d.model.Excerpt = t.Excerpt;
        d.model.PublishDate = t.PublishDate;
        d.model.PageCount = t.PageCount;

        d.control.fieldsReadOnly = true;
        d.control.btns.Nuevo.show = true;
        d.control.btns.Editar.show = true;
        d.control.btns.Cancelar.show = false;
        d.control.btns.Eliminar.show = true;
        d.control.btns.Guardar.show = false;
    };
    d.filterGeneral = function (item) {
        if (d.filter && item) {
            var str = item.ID + " " + item.Title + " " + item.Description + " "
                + item.Excerpt + " " + item.PageCount + " " + item.PublishDate;
            str = str.toUpperCase();
            var result = str.indexOf(d.filter.toUpperCase()) >= 0
            //(
            //    //(item.ID.toString() + "___").toUpperCase().indexOf(d.filter.toUpperCase()) >= 0
            //    //||
            //    (item.Title + "___").toUpperCase().indexOf(d.filter.toUpperCase()) >= 0
            //    //||
            //    //(item.Description + "___").toUpperCase().indexOf(d.filter.toUpperCase()) >= 0
            //    //||
            //    //(item.Excerpt + "___").toUpperCase().indexOf(d.filter.toUpperCase()) >= 0
            //    //||
            //    //(item.PageCount + "___").toUpperCase().indexOf(d.filter.toUpperCase()) >= 0
            //    //||
            //    //(item.PublishDate + "___").toUpperCase().indexOf(d.filter.toUpperCase()) >= 0
            //);
            return result;
        } else {
            return item;
        }
    }
    //#endregion SetSelected
    function xAlerta(msj) {
        //bootbox.alert(msj);
        alert(msj);
    }
    function xConfirm(msj, callBack) {
        var result = confirm(msj);
        if (result) {
            callBack();
        }

        //bootbox.confirm({
        //    message: msj,
        //    buttons: {
        //        confirm: {
        //            label: 'Sí',
        //            className: 'btn-success'
        //        },
        //        cancel: {
        //            label: 'No',
        //            className: 'btn-danger'
        //        }
        //    },
        //    callback: function (result) {
        //        if (result) {
        //            callBack();
        //        }
        //    }
        //});
    }
    d.startApp = function () {
        getData();
    };
};
booksApp.controller('booksController', ['$scope', '$http', 'booksDataFactory', booksController]);

