function updateTable(options) {
    function showElement(field) {
        options.hasOwnProperty(field) && $("#" + options[field]).show()
    }

    function hideElement(field) {
        options.hasOwnProperty(field) && $("#" + options[field]).hide()
    }

    function doAjax(url, waitingElement, isAutoupdate) {
        if (!updating) return hideElement("updatingErrorMsg"), showElement(waitingElement), updating = !0, null != seeMoreButton && seeMoreButton.addClass("disabled"), $.ajax({
            url: url,
            accepts: {
                json: "application/json"
            },
            dataType: "json",
            type: "GET",
            cache: !1,
            success: function(response) {
                void 0 === response[options.dataField] ? showElement("noDataRow") : ("function" == typeof options.processJSON && options.processJSON(response[options.dataField]), -1 == options.maxNumElements ? options.model.update(response[options.dataField], !0) : options.model.update(response[options.dataField], !1), hideElement("noDataRow"), isAutoupdate || response.hasOwnProperty("nextLink") && (showElement("seeMoreRow"), seeMoreURL = response.nextLink.replace(/^.*\/\/[^\/]+/, "")), (-1 == options.maxNumElements || options.maxNumElements > 0 && options.model.length >= options.maxNumElements) && hideElement("seeMoreRow")), options.hasOwnProperty("updateTime") && (autoUpdateTimer = setTimeout(automaticUpdate, options.updateTime))
            },
            error: function() {
                showElement("updatingErrorMsg"), options.hasOwnProperty("updateTime") && (autoUpdateTimer = setTimeout(automaticUpdate, 5 * options.updateTime))
            },
            complete: function() {
                hideElement(waitingElement), null != seeMoreButton && seeMoreButton.removeClass("disabled"), updating = !1
            }
        })
    }

    function automaticUpdate() {
        doAjax(options.url, "waitingAutoupdate", !0)
    }

    function update(url) {
        return hideElement("seeMoreRow"), null != autoUpdateTimer && window.clearTimeout(autoUpdateTimer), doAjax(url, "waitingRow", !1)
    }
    var seeMoreURL = null,
        updating = !1,
        autoUpdateTimer = null,
        seeMoreButton = null;
    options.hasOwnProperty("maxNumElements") || (options.maxNumElements = -1), options.hasOwnProperty("seeMoreRow") && (seeMoreButton = $("#" + options.seeMoreRow + " .btn"), seeMoreButton.on("click", function() {
        updating || update(seeMoreURL)
    })), update(options.url)
}