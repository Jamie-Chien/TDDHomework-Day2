﻿using PotterCart;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotterCartTest
{
    public class Cart
    {
        private List<Book> books;
        private readonly Dictionary<int, double> discountTable = null;
        private readonly int priceOfBook = 100;

        public Cart()
        {
            discountTable = new Dictionary<int, double>
            {
                { 1, 1 },
                { 2, 0.95 },
                { 3, 0.9 },
                { 4, 0.8 },
                { 5, 0.75 }
            };

            books = new List<Book>();
        }

        public void Add(Book book)
        {
            books.Add(book);
        }

        public int GetPrice()
        {
            return CalculatePrice(books, 0);
        }

        private int CalculatePrice(List<Book> books, int price)
        {
            if (books.Count > 0)
            {
                var groupedBooks = books.Distinct(x => x.Volume).ToList();

                var discount = discountTable[groupedBooks.Count];

                int groupedPice = Convert.ToInt32(groupedBooks.Count * priceOfBook * discount);

                var notCheckoutBooks = books.Where(x => !groupedBooks.Contains(x)).ToList();

                return groupedPice + CalculatePrice(notCheckoutBooks, price);
            }
            return price;
        }
    }
}