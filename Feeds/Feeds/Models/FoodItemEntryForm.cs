using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Feeds
{
    public class FoodItemEntryForm
    {
        public Entry DescriptionEntry { get; set; }
        public Entry QuantityEntry { get; set; }
        public Entry MeasurementEntry { get; set; }
        public BoxView Seperator { get; set; }

        public FoodItemEntryForm(List<FoodItem> foodItems, int foodItemCount)
        {
            DescriptionEntry = new Entry
            {
                FontSize = 8,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200,
                Placeholder = "Food Description",
                BindingContext = foodItems[foodItemCount]
            };
            DescriptionEntry.SetBinding(Entry.TextProperty, "Description");

            QuantityEntry = new Entry
            {
                FontSize = 8,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200,
                Placeholder = "Quantity",
                BindingContext = foodItems[foodItemCount]
            };
            QuantityEntry.SetBinding(Entry.TextProperty, "Quantity");

            MeasurementEntry = new Entry
            {
                FontSize = 8,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200,
                Placeholder = "Measurement (eg grams, cans, crates)",
                BindingContext = foodItems[foodItemCount]
            };
            MeasurementEntry.SetBinding(Entry.TextProperty, "Measurement");

            Seperator = new BoxView
            {
                Color = Color.Gray,
                HeightRequest = 1,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                Opacity = 0.5
            };
        }
    }
}
