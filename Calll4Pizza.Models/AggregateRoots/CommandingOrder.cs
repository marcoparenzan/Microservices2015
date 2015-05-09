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
    public class CommandingOrder : Call4PizzaAggregateRoot<CommandingOrder, Guid>, IAggregateRootSerializable
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

        public CommandingOrder()
        {
            _pizzas = new List<OrderPizza>();
            _beverages = new List<OrderBeverage>();
        }

        protected override Guid OnGetId()
        {
            return _commandId;
        }

        protected override void OnApply<TCommand>(TCommand command, ref bool handled)
        {
            if (command is CreateOrder)
            {
                Apply(command as CreateOrder);
                handled = true;
            }
        }

        protected void Apply(CreateOrder command)
        {
            ApplyPizza("PizzaMargherita", command.PizzaMargherita);
            ApplyPizza("PizzaCapricciosa", command.PizzaCapricciosa);
            ApplyPizza("PizzaDiavola", command.PizzaDiavola);
            ApplyBeverage("CocaCola", command.CocaCola);
            ApplyBeverage("Beer", command.Beer);
            _commandId = command.CommandId;
            _address = command.Address;
            _country = command.Country;
            _date = command.Date;
            _email = command.EMail;
            _firstName = command.FirstName;
            _lastName = command.LastName;
            _phone = command.Phone;
            _province = command.Province;
                
            Notify(new OrderCreated
            {
                Source = EventSource.Domain
                ,
                EventId = _commandId
            });
        }

        protected void ApplyPizza(string description, int quantity)
        {
            var item = _pizzas.SingleOrDefault(xx => xx.Description == description);
            if (item == null && quantity == 0) return;
            if (item == null && quantity > 0)
            {
                _pizzas.Add(new OrderPizza { Description = description, Quantity = quantity, UnitPrice = 5.00M });
                return;
            }
            if (item != null && quantity == 0)
            {
                _pizzas.Remove(item);
                return;
            }
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
                return;
            }

            throw new NotSupportedException("Condition Not Supported");
        }

        protected void ApplyBeverage(string description, int quantity)
        {
            var item = _beverages.SingleOrDefault(xx => xx.Description == description);
            if (item == null && quantity == 0) return;
            if (item == null && quantity > 0)
            {
                _beverages.Add(new OrderBeverage { Description = description, Quantity = quantity, UnitPrice = 5.00M });
                return;
            }
            if (item != null && quantity == 0)
            {
                _beverages.Remove(item);
                return;
            }
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
                return;
            }

            throw new NotSupportedException("Condition Not Supported");
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
                    writer.WritePropertyName("Address"); writer.WriteValue(_address);
                    writer.WritePropertyName("Country"); writer.WriteValue(_country);
                    writer.WritePropertyName("Date"); writer.WriteValue(_date);
                    writer.WritePropertyName("Email"); writer.WriteValue(_email);
                    writer.WritePropertyName("FirstName"); writer.WriteValue(_firstName);
                    writer.WritePropertyName("LastName"); writer.WriteValue(_lastName);
                    writer.WritePropertyName("Phone"); writer.WriteValue(_phone);
                    writer.WritePropertyName("Province"); writer.WriteValue(_province);
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
                    throw new NotSupportedException("Property not recognized");
            }
        }
    }
}
