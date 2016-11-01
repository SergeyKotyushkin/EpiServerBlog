define([
    "dojo/_base/declare",
    "dojo/dom-style",
    "dojo/aspect",

    "epi-cms/widget/ContentSelector"
],

function (
    declare,
    domStyle,
    aspect,

    ContentSelector
) {
    return declare("app/Editors/ImageContentSelector", [ContentSelector], {

        thumbnail: null,

        content: null,

        constructor: function () {
            this.inherited(arguments);

            // List for when the previewUrl property is set through the inherited _setValueAndFireOnChange method
            aspect.after(this, "set", function (name, value) {
                if (name === 'previewUrl' && value) {
                    this.updateThumbnail();
                }

                // No need to return - set method does not return anything
            }, true);

            // Create thumbnail element
            this.thumbnail = dojo.create("img");

            domStyle.set(this.thumbnail, 'borderBottom', '1px solid #929ba4');

            domStyle.set(this.thumbnail, 'display', 'none');
        },

        postCreate: function () {

            this.inherited(arguments);

            dojo.place(this.thumbnail, this.displayNode, "first");
        },

        _updateDisplayNode: function (content) {

            // Inherited from _SelectorBase through ContentSelector

            this.inherited(arguments);

            this.content = content;

            this.updateThumbnail();
        },

        updateThumbnail: function () {

            var content = this.content;

            var thumbnail = this.thumbnail;

            var clearThumbnail = function () {
                dojo.attr(thumbnail, 'src', '');
                domStyle.set(thumbnail, 'display', 'none');
            }

            if (content && thumbnail) {
                if (content.previewUrl && content.publicUrl) {

                    var previewUrlWithScaling = content.previewUrl;

                    var fileName = content.publicUrl.substr(content.publicUrl.lastIndexOf("/") + 1);

                    var fileExtension;

                    var indexOfLastDot = fileName.lastIndexOf('.');

                    if (indexOfLastDot != -1) {
                        fileExtension = fileName.substring(indexOfLastDot + 1);
                    }

                    if (!fileExtension) {

                        var previewUrlSuffix = ',,' + content.contentLink;

                        var endOfFileNamePosition = content.previewUrl.lastIndexOf(previewUrlSuffix);

                        if (endOfFileNamePosition != -1) {

                            // Assume .jpg extension if no file extension in URL
                            previewUrlWithScaling = content.previewUrl.substring(0, endOfFileNamePosition) + '.jpg' + content.previewUrl.substring(endOfFileNamePosition);
                        }
                    }

                    previewUrlWithScaling += '&width=195';

                    dojo.attr(thumbnail, 'src', previewUrlWithScaling);
                    dojo.attr(thumbnail, 'width', "195");
                    domStyle.set(thumbnail, 'display', '');
                }
                else {
                    clearThumbnail();
                }
            } else {
                clearThumbnail();
            }
        }
    });
});