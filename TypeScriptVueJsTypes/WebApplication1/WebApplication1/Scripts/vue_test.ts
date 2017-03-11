import * as Vue from "../typings/vuejs";

namespace Test
{
	export class MyClass
	{
		public initialize()
		{
			var component = this.getComponent();
		}

		private getComponent(): Vue.Component
        {
			return Vue.Component("test", {
				template: "<div></div>",
				props: ["test"],
				methods: {
					onClick: () =>
					{
					}
				}
			});
		}
	}
}