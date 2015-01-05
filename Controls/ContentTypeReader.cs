using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomShane.Neoforce.Controls
{
    public class ContentTypeReader<T>
    {
        protected virtual T Read(ContentReader input, T existingInstance)
        {
            return existingInstance;
        }

    }
}
