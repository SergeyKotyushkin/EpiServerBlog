(function($) {
    var bindEvents = function(e, gadget) {
        $("input[type=checkbox]").each(function() {

            $(this).change(function() {
                var isChecked = $(this).is(":checked") ? "1" : "0";
                var pageId = $(this).attr("data-page");
                var commentId = $(this).attr("data-comment");

                var data = { isChecked: isChecked, pageId: pageId, commentId: commentId };

                gadget.ajax({
                    type: "POST",
                    url: gadget.getActionPath({ action: "Save" }),
                    data: { data: JSON.stringify(data) },
                    feedbackMessage: "Saving",
                    success: function(data) {
                        gadget.setFeedbackMessage(data === "0" ? "Fail" : "Success");
                    }
                });
            });
        });
    };
    playground = {
        init: function(e, gadget) {
            $(this).bind("epigadgetloaded", bindEvents);
        }
    };
})(epiJQuery);