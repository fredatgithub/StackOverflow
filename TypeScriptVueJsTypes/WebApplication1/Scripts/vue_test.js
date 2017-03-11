"use strict";
var Test;
(function (Test) {
    var MyClass = (function () {
        function MyClass() {
        }
        MyClass.prototype.initialize = function () {
            var component = this.getComponent();
        };
        MyClass.prototype.getComponent = function () {
            return;
            //return Vue.ComponentOptions("test", {
            //	template: "<div></div>",
            //	props: ["test"],
            //	methods: {
            //		onClick: () =>
            //		{
            //		}
            //	}
            //});
        };
        return MyClass;
    }());
    Test.MyClass = MyClass;
})(Test || (Test = {}));
//# sourceMappingURL=vue_test.js.map