$(document).ready(function () {

    var editor = CKEDITOR.instances["comment"];
    if (editor) {
        editor.destroy(true);
    }

    CKEDITOR.replace("comment", {
        height: 100,
        extraPlugins: "bbcode,smiley,font,colorbutton,autogrow",
        removePlugins: "filebrowser,format,horizontalrule,pastetext,pastefromword,scayt,showborders,stylescombo,table,tabletools,wsc",
        removeButtons: "BGColor,Font,Strike,Subscript,Superscript",
        disableObjectResizing: true,
        fontSize_sizes: "30/30%;50/50%;100/100%;120/120%;150/150%;200/200%;300/300%",
        smiley_images: [
            "regular_smile.png", "sad_smile.png", "wink_smile.png", "teeth_smile.png", "tongue_smile.png",
            "embarrassed_smile.png", "omg_smile.png", "whatchutalkingabout_smile.png", "angel_smile.png",
            "shades_smile.png", "cry_smile.png", "kiss.png"
        ],
        smiley_descriptions: [
            "smiley", "sad", "wink", "laugh", "cheeky", "blush", "surprise",
            "indecision", "angel", "cool", "crying", "kiss"
        ]
    });

    function updateComments(viewModel) {
        for (var i = 0; i < comments.length; i++) {
            viewModel.addComment(comments[i]);
        }
    }

    var commentEditorConfig = {
        height: 100,
        extraPlugins: "bbcode",
        readOnly: true,
        removePlugins: "elementspath,toolbar,autogrow",
        resize_enabled: false,
        autoGrow_bottomSpace: false
    };


    // knockout view model
    function commentViewModel() {
        var self = this;

        self.text = ko.observable(null);
        self.username = ko.observable(null);
        self.date = ko.observable(null);
        self.order = ko.observable(null);
    }

    function articleViewModel() {
        var self = this;

        self.orderByDate = ko.observable(true);
        self.isOrdered = ko.observable(true);

        self.comments = ko.observableArray([]);

        self.orderByDate.subscribe(function () {
            self.isOrdered(false);

            $(".all-comments").each(function() {
                CKEDITOR.instances[$(this).attr("id")].destroy(true);
            });

            self.comments.removeAll();

            comments.reverse();
            updateComments(self);

            setTimeout(function() {
                self.isOrdered(true);
            }, 500);

        });

        self.addComment = function(comment) {
            var newComment = new commentViewModel();
            newComment.text(comment.Text);
            newComment.username(comment.Username);
            newComment.date(comment.DateOutput);
            newComment.order(comment.Order);

            self.comments.push(newComment);
        }
        
        self.afterCommentRender = function(element) {
            var id = $(element).find("textarea").attr("id");

            if (CKEDITOR.instances[id]) {
                CKEDITOR.instances[id].destroy(true);
            }

            CKEDITOR.replace(id, commentEditorConfig);
        }
    }

    var viewModel = new articleViewModel();
    ko.applyBindings(viewModel);

    updateComments(viewModel);
});