"use strict";
var apiUrl = '/api/posts/';
var postsToday = 0;
var projectId;
var postsList;
var domReady = false;
var dataReceived = false;

loadPosts();

function loadPosts() {
    projectId = $('#projectid').data("name");
    $.getJSON(apiUrl + projectId).done(function (data) {
        postsList = data.filter(function (obj) {
            return obj.ScreenName != "BriansWebWorks" // temp hack to make the team happy 
        });
        calculateStats();
        if (domReady) displayPageContent();
    });
}

function displayPageContent() {
    $('#postsTemplate').tmpl().appendTo($('#posts'));
    var $postsContainer = $('#posts');
    $postsContainer.masonry({
        itemSelector: '.post',
        gutter: 15,
        isAnimated: true,
        isFitWidth: true,
    });

    displayStats();
    displayRefreshTime();
}

function calculateStats() {
    for (var postIndex in postsList) {
        if (DateUtils.isToday(new Date(postsList[postIndex].DateCreated)))
            postsToday++;
    }
}

$(document).ready(function () {
    domReady = true;
    if (dataReceived) displayPageContent();
});

function displayStats() {
    if (postsToday >= 50)
        $('<span>', { text: '50+ posts today!' }).appendTo($('#stats'));
    else
        $('<span>', { text: postsToday + ' posts today' }).appendTo($('#stats'));
}

function displayRefreshTime() {
    $('<span>', { text: 'Last refreshed at ' + DateUtils.toShortTimeString(new Date()) }).appendTo($('#last-refresh'));
}

function getPostTimeText(datePosted) {
    var date = new Date(datePosted);
    return DateUtils.toShortTimeString(date) + (DateUtils.isToday(date) ? '' : ' on ' + DateUtils.toShortDateString(date));
}

function unencodeString(str) {
    return str.replace(/&quot;/g, '"').replace(/&amp;/g, "&").replace(/&lt;/g, "<").replace(/&gt;/g, ">");
}