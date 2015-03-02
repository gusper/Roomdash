"use strict";

var DateUtils = {
    toShortDateString: function (date) {
        return date.getMonth() + 1 + '/' + date.getDate();
    },

    toShortTimeString: function (date) {
        var hours = date.getHours();
        var suffix = (hours >= 12) ? 'p' : 'a';
        hours = (hours > 12) ? hours - 12 : hours;
        if (hours === 0) hours = 12;
        var minutes = date.getMinutes();
        minutes = (minutes < 10 ? '0' : '') + minutes;
        return hours + ':' + minutes + suffix;
    },

    daysSince: function (refDate) {
        return DateUtils.calcDaysBetween(new Date(), refDate);
    },

    isToday: function (date) {
        return DateUtils.isSameDate(new Date(), date);
    },

    isSameDate: function (d1, d2) {
        return d1.getDate() === d2.getDate() && d1.getMonth() === d2.getMonth() && d1.getFullYear() === d2.getFullYear();
    },

    calcDaysBetween: function (d1, d2) {
        var oneDay = 24 * 60 * 60 * 1000;

        var d1Ms = d1.getTime();
        var d2Ms = d2.getTime();
        return Math.round(Math.abs(d1Ms - d2Ms) / oneDay);
    },
}