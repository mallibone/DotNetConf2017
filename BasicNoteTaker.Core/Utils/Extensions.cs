using Newtonsoft.Json;

namespace BasicNoteTaker.Core.Utils
{
	public static class Extensions
	{
		public static T Clone<T>(this T toBeCloned)
		{
			var cloneString = JsonConvert.SerializeObject(toBeCloned);
			return JsonConvert.DeserializeObject<T>(cloneString);
		}
	}
}
