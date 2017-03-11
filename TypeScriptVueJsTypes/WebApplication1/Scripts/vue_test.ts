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
			return Vue.component("test", {
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