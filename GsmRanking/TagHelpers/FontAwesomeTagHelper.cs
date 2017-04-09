using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GsmRanking.TagHelpers
{
    [HtmlTargetElement("fa", TagStructure = TagStructure.WithoutEndTag)]
    public class FontAwesomeTagHelper : TagHelper
    {
        [HtmlAttributeName("class")]
        public string Classes { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ProcessAsync(context, output).Wait();
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.Attributes.SetAttribute("aria-hidden", true);
            output.Attributes.SetAttribute("class", $"fa {Classes}");
        }
    }
}
