using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Dynamic;

namespace UriTester
{
    


    // XElement extensions
    public static class XElementExtensions
    {
        // extended the XElement with a method called DoDynamicList
        public static List<dynamic> ToDynamicList(this XElement elements, List<dynamic> data = null)
        {
            // if we already have items in the data object, we will append to them
            // if not create a new data object
            if (data == null)
            {
                data = new List<dynamic>();
            }

            // loop through child elements
            foreach (XElement element in elements.Elements())
            {
                // define an Expando Dynamic
                dynamic person = new ExpandoObject();

                // cater for attributes as properties
                if (element.HasAttributes)
                {
                    foreach (var attribute in element.Attributes())
                    {
                        ((IDictionary<string, object>)person).Add(attribute.Name.LocalName, attribute.Value);
                    }
                }

                // cater for child nodes as properties, or child objects
                if (element.HasElements)
                {
                    foreach (XElement subElement in element.Elements())
                    {
                        // if sub element has child elements
                        if (subElement.HasElements)
                        {
                            // using a bit of recursion lets us cater for an unknown chain of child elements
                            ((IDictionary<string, object>)person).Add(subElement.Name.LocalName, subElement.ToDynamicList());
                        }
                        else
                        {
                            ((IDictionary<string, object>)person).Add(subElement.Name.LocalName, subElement.Value);
                        }
                    }
                }

                data.Add(person);
            }

            return data;
        }
    }
}
