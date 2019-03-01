using System.Net.Http;

namespace Cumulocity.SDK.Client.Rest.Utils
{
    public static class StringContentExt {

        public static StringContent Replace(this StringContent content, string mediatype){

            var type = "content-type";
            var removed = content.Headers.Remove(type);
            
            if(removed){
                content.Headers.TryAddWithoutValidation(type,mediatype);
            }
		
            return content;
        }
    }
}