﻿define([
    "dojo/_base/declare",
    "dojo/aspect",
    "dojo/dom-construct",
    "dojo/dom-attr",
    "dojo/dom-style",
    "dojo/_base/connect",
    "dojo/_base/lang",
    "dojo/query",

    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

    "dijit/form/Button",
    "dijit/form/Select",
    "dijit/form/TextBox",

    "dojo/i18n!./nls/Labels",

    'xstyle/css!./Template.css'
],

function (
    declare,
    aspect,
    domConstruct,
    domAttr,
    domStyle,
    connect,
    lang,
    query,

    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

    Button,
    Select,
    TextBox,

    Labels
) {
    return declare("app.Editors.stringlist.Editor", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin], {

        templateString: dojo.cache("app.Editors.stringlist", "Template.html"),

        labels: Labels,

        value: null,

        _hasSelectionFactory: false,

        constructor: function () {
            this.inherited(arguments);

            // When the property value is set, we refresh the DOM elements representing the strings in the list
            aspect.after(this, '_set', lang.hitch(this, function () {
                this._refreshStringElements(this.value);
            }));
        },

        postCreate: function () {
            this.inherited(arguments);

            // summary: Populates the dropdown (if selection factory options are available), otherwise the textbox is displayed

            if (this.selections && this.selections.length > 0) {
                this._hasSelectionFactory = true;
            }

            if (this._hasSelectionFactory) {
                for (var i = 0; i < this.selections.length; i++) {

                    var item = this.selections[i];

                    this.stringSelector.addOption({
                        disabled: false,
                        label: (item.text && item.text !== '') ? item.text : '&nbsp',
                        selected: false,
                        value: item.value
                    });
                }

                // Only display dropdown when we have a selection factory attached
                domStyle.set(this.stringTextbox.domNode, 'display', 'none');
            } else {
                // Only display textbox when there is no selection factory attached
                domStyle.set(this.stringSelector.domNode, 'display', 'none');
            }

            this.stringSelector.setDisabled(this.readOnly);
            this.stringTextbox.setDisabled(this.readOnly);
            this.addButton.setDisabled(true); // Disable add button by default, until string is selected or entered
        },

        onChange: function (value) {
            this.inherited(arguments);

            // summary: Notifies Episerver that the property value has changed
        },

        _setValue: function () {

            // summary: Sets the property value based on the strings added

            var strings = this._getAddedStrings();

            this.set("value", strings.length > 0 ? strings : null);

            this._setHelpTextVisibility();

            this.onChange(strings);
        },

        _refreshStringElements: function (strings) {

            // summary: Make the list of strings match the property (widget) value

            if (strings === undefined || strings === null) {
                return;
            }

            var that = this;

            strings.forEach(function (string, index, array) {
                if (strings.indexOf(string) === -1) {
                    that._removeStringElement(string);
                }
            });

            // Add an element for each string in the list
            strings.forEach(function (string, index, array) {

                var displayName = that._getStringDisplayName(string);

                that._addStringElement(string, displayName);
            });

            this._setHelpTextVisibility();
        },

        _setHelpTextVisibility: function () {
            // summary: Determines whether the help text, indicating that the list is empty, should be displayed

            if (!this.value || this.value.length === 0) {
                domStyle.set(this.helpText, 'display', 'inline');
            } else {
                domStyle.set(this.helpText, 'display', 'none');
            }
        },

        _onTextboxKeyUp: function (e) {

            // summary: Handles when a keyboard key is pressed in the string textbox, primarily to enable/disable the "+" button (when not using a dropdown for a selection factory)

            var value = e.target.value.trim();

            this.addButton.setDisabled(value.trim() === '');
        },

        _onTextboxKeyDown: function (e) {

            // summary: Handles when a keyboard key is pressed in the string textbox, primarily to add a string when Enter is pressed (when not using a dropdown for a selection factory)

            if (e.keyCode === 13) // Enter
            {
                e.target.blur();

                this._addString(e.target.value.trim());
            }
        },

        _selectedStringChanged: function (value) {

            // summary: Handles when the selected string in the dropdown changes

            if (value) {
                this.addButton.setDisabled(false);
            }
        },

        _onRemoveClick: function (e) {

            // summary: Handles when a remove ("x") button is clicked

            // Get the string value that was clicked
            var stringValue = domAttr.get(e.srcElement, "data-value").trim();

            this._removeStringElement(stringValue);

            this._setValue();
        },

        _onAddButtonClick: function () {

            // summary: Handles when the add ("+") button is clicked

            if (this._hasSelectionFactory) { // Add string selected in dropdown
                var selectedValue = this.stringSelector.value;
                var displayName = this.stringSelector.focusNode.innerText;

                if (!selectedValue) {
                    return;
                }

                this._addString(selectedValue, displayName);
            } else { // Add string from textbox

                var enteredValue = this.stringTextbox.value;

                if (!enteredValue) {
                    return;
                }

                this._addString(enteredValue);
            }
        },

        _getStringElements: function () {

            // summary: Gets all DOM elements representing added strings

            return query(".epi-categoryButton", this.valuesContainer);
        },

        _getAddedStrings: function () {

            // summary: Gets the values of all DOM elements representing added strings

            var elements = this._getStringElements();

            var strings = [];

            elements.forEach(function (element, index, array) {
                strings.push(domAttr.get(element, 'data-value'));
            });

            return strings;
        },

        _addString: function (value, displayName) {

            // summary: Adds a string to the list and updates the property value

            value = value.trim();

            if (!value) {
                return;
            }

            if (!displayName) {
                displayName = value;
            }

            this._addStringElement(value, displayName);

            this.stringTextbox.set('value', ""); // Reset textbox value

            this._setValue();
        },

        _addStringElement: function (value, displayName) {

            // summary: Adds a DOM element representing a string in the list

            if (!value) {
                return;
            }

            value = value.trim();

            if (value === '') {
                return;
            }

            if (!displayName) {
                displayName = value;
            }

            // Don't add if it's already added
            if (query("div[data-value=" + value + "]", this.valuesContainer).length !== 0) {
                return;
            }

            var containerDiv = domConstruct.create('div', { 'class': 'epi-categoryButton' });
            var buttonWrapperDiv = domConstruct.create('div', { 'class': 'dijitInline epi-resourceName' });
            var categoryNameDiv = domConstruct.create('div', { 'class': 'dojoxEllipsis', innerHTML: displayName });

            domConstruct.place(categoryNameDiv, buttonWrapperDiv);

            domConstruct.place(buttonWrapperDiv, containerDiv);

            var removeButtonDiv = domConstruct.create('div', { 'class': 'epi-removeButton', innerHTML: '&nbsp;', title: Labels.clickToRemove });

            var eventName = removeButtonDiv.onClick ? 'onClick' : 'onclick';

            // Add attributes to make added values easy to find and remove
            domAttr.set(containerDiv, 'data-value', value);
            domAttr.set(removeButtonDiv, 'data-value', value);

            if (!this.readOnly) {
                this.connect(removeButtonDiv, eventName, lang.hitch(this, this._onRemoveClick));
                domConstruct.place(removeButtonDiv, buttonWrapperDiv);
            } else {
                domConstruct.place(domConstruct.create("span", { innerHTML: "&nbsp;" }), buttonWrapperDiv);
            }

            domConstruct.place(containerDiv, this.valuesContainer);
        },

        _removeStringElement: function (value) {

            // summary: Removes the DOM element, if any, representing a string in the list

            if (value.trim() === '') {
                return;
            }

            var matchingValues = query("div[data-value=" + value + "]", this.valuesContainer);

            for (var i = 0; i < matchingValues.length; i++) {
                domConstruct.destroy(matchingValues[i]);
            }
        },

        _getStringDisplayName: function (string) {

            // summary: Looks up a string value among the selection factory options, returning the corresponding display name if found

            if (!this._hasSelectionFactory) {
                return string;
            }

            var displayName = string;

            this.selections.some(function (selection) {
                if (selection.value === string) {
                    if (selection.text) {
                        displayName = selection.text;
                    }

                    return true; // Break
                }
            });

            return displayName;
        }
    });
});