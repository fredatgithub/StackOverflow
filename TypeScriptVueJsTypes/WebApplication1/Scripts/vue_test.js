"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = {
    template: '<button @click="onClick">Click!</button>',
    data: function () {
        return {
            message: 'Hello!'
        };
    },
    methods: {
        onClick: function () {
            window.alert(this.message);
        }
    }
};
