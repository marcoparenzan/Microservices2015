using Call4Pizza.Models.Commands;
using Call4Pizza.Models.DTO;
using Call4Pizza.Models.Entities;
using Call4Pizza.Models.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.AggregateRoots
{
    public class QueryingOrder : Call4PizzaAggregateRoot<QueryingOrder, Guid>, IAggregateRootSerializable
    {
        protected List<OrderPizza> _pizzas;
        protected List<OrderBeverage> _beverages;

        protected Guid _commandId;
        protected string _address;
        protected string _country;
        protected DateTime _date;
        protected string _email;
        protected string _firstName;
        protected string _lastName;
        protected string _phone;
        protected string _province;

        public QueryingOrder()
        {
            _pizzas = new List<OrderPizza>();
            _beverages = new List<OrderBeverage>();
        }

        protected override Guid OnGetId()
        {
            return _commandId;
        }

        void IAggregateRootSerializable.SerializeTo(Stream stream)
        {
             var serializer = new JsonSerializer();
             serializer.Converters.Add(new JavaScriptDateTimeConverter());
             serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var sw = new StreamWriter(stream))
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("CommandId"); writer.WriteValue(_commandId);
                    writer.WritePropertyName("Pizzas"); serializer.Serialize(writer, _pizzas);
                    writer.WritePropertyName("Beverages"); serializer.Serialize(writer, _beverages);
                    writer.WriteEndObject();
                }
            }
        }

        void IAggregateRootSerializable.DeserializeFrom(System.IO.Stream stream)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var sr = new StreamReader(stream))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    while(reader.TokenType != JsonToken.StartObject)  reader.Read();
                    reader.Read();
                    while (true)
                    {
                        if (reader.TokenType == JsonToken.EndObject)
                        {
                            reader.Read();
                            break;
                        }

                        switch (reader.TokenType)
                        {
                            case JsonToken.PropertyName:
                                DeserializeProperty(serializer, reader, reader.Value as string);
                                break;
                            case JsonToken.Comment:
                                reader.Read();
                                break;
                            default:
                                throw new NotSupportedException("Token not recognized");
                        }
                    }
                }
            }
        }

        private void DeserializeProperty(JsonSerializer serializer, JsonTextReader reader, string propertyName)
        {
            switch (propertyName)
            {
                case "CommandId":
                    _commandId = Guid.Parse(reader.ReadAsString());reader.Read();
                    break;
                case "Address":
                    _address = reader.ReadAsString(); reader.Read();
                    break;
                case "Country":
                    _country = reader.ReadAsString(); reader.Read();
                    break;
                case "Date":
                    _date = reader.ReadAsDateTime().Value; reader.Read();
                    break;
                case "Email":
                    _email = reader.ReadAsString(); reader.Read();
                    break;
                case "FirstName":
                    _firstName = reader.ReadAsString(); reader.Read();
                    break;
                case "LastName":
                    _lastName = reader.ReadAsString(); reader.Read();
                    break;
                case "Phone":
                    _phone = reader.ReadAsString(); reader.Read();
                    break;
                case "Province":
                    _province = reader.ReadAsString(); reader.Read();
                    break;
                case "Pizzas":
                    reader.Read();
                    _pizzas = serializer.Deserialize<List<OrderPizza>>(reader);reader.Read();
                    break;
                case "Beverages":
                    reader.Read();
                    _beverages = serializer.Deserialize<List<OrderBeverage>>(reader);reader.Read();
                    break;
                default:
                    // skip unuseful properties
                    reader.ReadAsString(); reader.Read();
                    // throw new NotSupportedException("Property not recognized");
                    break;
            }
        }

        public IEnumerable<PizzaToDoDTO> PizzasToDo()
        {
            return _pizzas.Select(xx => new PizzaToDoDTO { 
                Description = xx.Description
                ,
                Quantity = xx.Quantity
                ,
                Date = _date
                ,
                CommandId = _commandId
                ,
                Name = string.Format("{0} {1}", _lastName, _firstName)
                ,
                City = _province
            }).ToList();
        }
    }
}
