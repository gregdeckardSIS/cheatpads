﻿define(["ko", "ajax"], function (ko, ajax) {
    var model = {};

    model.users = ko.observableArray();
    model.user = ko.observable();
    model.userJSON = ko.pureComputed(function () {
        return JSON.stringify(model.user(), null, '  ')
    });

    model.init = function () {
        ajax.users.get("users").as(model.users);
    }

    model.select = function (data) {
        ajax.users.get("users", data.id).done(function (user) {
            model.user(user);
        });
    }

    return model;
});