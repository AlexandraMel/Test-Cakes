using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CakeExchange.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CakeExchange.Controllers
{
    public class HomeController : Controller
    {
        private CakeContext db;

        public HomeController(CakeContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.offers = db.Offers.OrderByDescending(x => x.Price);
            ViewBag.purchases = db.Purchase.OrderBy(x => x.Price);
            ViewBag.history = db.History.OrderBy(x => x.Date);


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffers(Offer orderOffer)
        {
            // пока такая обработка ошиб
            if (orderOffer.Price <= 0 || orderOffer.Count <= 0)
                return RedirectToAction("Index");

            orderOffer.Date = DateTime.Now;

            var purchases = db.Purchase.Where(x => x.Price >= orderOffer.Price).OrderBy(y => y.Date)
                .Take(orderOffer.Count).ToList();

            var count = 0;

            foreach (var purchase in purchases)
            {
                count += purchase.Count;

                if (count <= orderOffer.Count)
                {
                    await db.History.AddAsync(new History
                    {
                        Date = DateTime.Now,
                        DatePurchase = purchase.Date,
                        DateOffer = orderOffer.Date,
                        Price = orderOffer.Price,
                        Count = purchase.Count,
                        EmailPurchase = purchase.Email,
                        EmailOffer = orderOffer.Email
                    });
                    await db.SaveChangesAsync();

                    db.Purchase.Remove(purchase);
                    await db.SaveChangesAsync();

                    if (count == orderOffer.Count) break;
                }
                else
                {
                    var remain = purchase.Count - (count - orderOffer.Count);

                    await db.History.AddAsync(new History
                    {
                        Date = DateTime.Now,
                        DatePurchase = purchase.Date,
                        DateOffer = orderOffer.Date,
                        Price = orderOffer.Price,
                        Count = remain,
                        EmailPurchase = purchase.Email,
                        EmailOffer = orderOffer.Email
                    });

                    purchase.Count -= remain;
                    db.Purchase.Update(purchase);
                    await db.SaveChangesAsync();

                    break;
                }
            }

            if (count < orderOffer.Count)
            {
                orderOffer.Count -= count;
                orderOffer.Price = Math.Round(orderOffer.Price, 2);
                await db.Offers.AddAsync(orderOffer);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase(Purchase orderPurchase)
        {
            // пока такая обработка 
            if (orderPurchase.Price <= 0 || orderPurchase.Count <= 0)
                return RedirectToAction("Index");

            var count = 0;
            orderPurchase.Date = DateTime.Now;

            var offers = db.Offers.Where(x => x.Price <= orderPurchase.Price).OrderBy(y => y.Price).Take(orderPurchase.Count).ToList();

            foreach (var offer in offers)
            {
                count += offer.Count;

                if (count <= orderPurchase.Count) 
                {
                    await db.History.AddAsync(new History
                    {
                        Date = DateTime.Now,
                        DatePurchase = orderPurchase.Date,
                        DateOffer = offer.Date,
                        Price = offer.Price,
                        Count = offer.Count,
                        EmailPurchase = orderPurchase.Email,
                        EmailOffer = offer.Email
                    });
                    await db.SaveChangesAsync();

                    db.Offers.Remove(offer);
                    await db.SaveChangesAsync();

                    if (count == orderPurchase.Count) break;
                }
                else 
                {
                    var remain = offer.Count - (count - orderPurchase.Count); 

                    await db.History.AddAsync(new History
                    {
                        Date = DateTime.Now,
                        DatePurchase = orderPurchase.Date,
                        DateOffer = offer.Date,
                        Price = offer.Price,
                        Count = remain,
                        EmailPurchase = orderPurchase.Email,
                        EmailOffer = offer.Email
                    });

                    offer.Count -= remain;
                    db.Offers.Update(offer);
                    await db.SaveChangesAsync();

                    break;
                }
            }

            if (count < orderPurchase.Count)
            {
                orderPurchase.Count -= count;
                orderPurchase.Price = Math.Round(orderPurchase.Price, 2);
                await db.Purchase.AddAsync(orderPurchase);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
            
    }
} 
