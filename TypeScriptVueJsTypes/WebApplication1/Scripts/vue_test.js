var Test;
(function (Test) {
    class MyClass {
        initialize() {
        }
        getComponent() {
            return Vue.component("test", {
                template: "<div></div>",
                props: ["test"],
                methods: {
                    onClick: () => {
                    }
                }
            });
        }
    }
    Test.MyClass = MyClass;
})(Test || (Test = {}));
//# sourceMappingURL=vue_test.js.map