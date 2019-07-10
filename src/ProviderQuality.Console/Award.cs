using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console
{
    public class Award
    {
        public string Name { get; set; }

        public int ExpiresIn { get; set; }

        public int Quality { get; set; }

        public void DecreaseExpirationDate()
        {
            ExpiresIn -= 1;
        }

        public void ChangeQuality(int quantityToChangeQualityBy)
        {
            if (Quality > 0)
            {
                if (ExpiresIn > 0)
                {
                    Quality += quantityToChangeQualityBy;
                }
                else
                {
                    Quality += (quantityToChangeQualityBy * 2);
                }
            }
            else
            {
                Quality = 0;
            }
        }
    }
}
