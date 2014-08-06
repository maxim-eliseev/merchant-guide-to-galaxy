namespace MerchantGuideToGalaxy.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Utils;

    public class GoodPriceResponderTask : ITask
    {
        private readonly Context context;

        private readonly AlienToArabicConvertor alienToArabicConvertor;

        public GoodPriceResponderTask(Context context)
        {
            this.context = context;
            alienToArabicConvertor = new AlienToArabicConvertor(context);
        }

        ////  how many Credits is glob prok Silver ?
        public void Run(string inputLine)
        {
            var preparedInputline = Regex.Replace(inputLine, LineParsingUtility.GoodsPriceQuestionStart, string.Empty, RegexOptions.IgnoreCase);
            preparedInputline = preparedInputline.Replace("?", string.Empty);
            preparedInputline = preparedInputline.Trim();
            var alienNumberWithGoodsNameString = preparedInputline;

            string[] alienNumberWithGoodsName = alienNumberWithGoodsNameString.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var goodsName = alienNumberWithGoodsName.Last();
            var alienNumber = alienNumberWithGoodsName.WithoutLast();

            if (!context.GoodsPricesPerUnit.ContainsKey(goodsName))
            {
                throw new ArgumentException("Unknown goods name:" + goodsName);
            }
            decimal priceOfUnit = context.GoodsPricesPerUnit[goodsName];

            var arabicNumber = alienToArabicConvertor.Convert(alienNumber);
            int numberOfUnits = arabicNumber;

            decimal totalPrice = priceOfUnit * numberOfUnits;

            var alienNumberAsString = string.Join(" ", alienNumber);
            var response = string.Format("{0} {1} is {2} Credits", alienNumberAsString, goodsName, totalPrice);
            context.Output.Add(response);
        }
    }
}
