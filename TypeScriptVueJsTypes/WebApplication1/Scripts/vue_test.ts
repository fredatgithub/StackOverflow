import * as Vue from "../typings/vuejs";

interface MyComponent extends Vue {
    message: string
    onClick(): void
}

export default {
    template: '<button @click="onClick">Click!</button>',
    data: function () {
        return {
            message: 'Hello!'
        }
    },
    methods: {
        onClick: function () {
            window.alert(this.message)
        }
    }
}