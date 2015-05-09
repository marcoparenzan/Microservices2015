using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models
{
    public interface IAggregateRootSerializable
    {
        void SerializeTo(Stream stream);
        void DeserializeFrom(Stream stream);
    }
}
