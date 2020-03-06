using Stubble.Core.Builders;
using System;
using System.Activities;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Mustache.Activities.UiPath
{
    public class TextFromTemplate : CodeActivity
    {
        [Category("Input")]
        public InArgument<string> TemplateString { get; set; }

        [Category("Input")]
        public InArgument<string> TemplatePath { get; set; }
        
        [Category("Input")]
        public InArgument<object> InputObject { get; set; }

        [Category("Output")]
        public OutArgument<string> Output { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var templateString = TemplateString.Get(context);
            var templatePath = TemplatePath.Get(context);
            var obj = InputObject.Get(context);
            var stubble = new StubbleBuilder().Build();
            string output;

            if (!string.IsNullOrEmpty(templateString))
            {
                output = stubble.Render(templateString, obj);
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(templatePath, Encoding.UTF8))
                {
                    output = stubble.Render(streamReader.ReadToEnd(), obj);
                }
            }
            
            
            Output.Set(context, output);
        }
    }
}
