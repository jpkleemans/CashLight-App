using CashLight_App.Tables;
using CashLight_App.Services.Interface;
using CashLight_App.Views.Categorize;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using CashLight_App.Models;

namespace CashLight_App.ViewModels
{
    public class CategorizeViewModel
    {
        private CategorizeView _view;
        private Transaction _transactionModel;
        private List<Transaction> _transactions;
        public Transaction _currentTransaction;
        private IUnitOfWork _unitOfWork;
        public CategorizeViewModel(CategorizeView categorizeView, IUnitOfWork unitOfWork)
        {
            this._view = categorizeView;
            this._unitOfWork = unitOfWork;
            _transactionModel = new Transaction();
            _transactions = _unitOfWork.Transaction.FindAll().Where(q => !q.CategoryID.HasValue).ToList();
        }

        /// <summary>
        /// Hides the current transaction and shows the next transaction
        /// </summary>
        public void ShowNextTransactionView(int i)
        {              
            if (_transactions.Count > 0)
            {
                Transaction nextTransaction = _transactions.First(); // Get the next transaction from the list

                _currentTransaction = nextTransaction;

                Control viewToShow = new TransactionView(nextTransaction); // Create new TransactionView for next transaction
                viewToShow.Location = new Point(-viewToShow.Width, 150);
                _view.Controls.Add(viewToShow);

                _transactions.Remove(nextTransaction); // Remove transaction from list, so the next transaction becomes the first of the list
            }
            else
            {
                Kernel.MainViewModel.ShowView(new SpendingsView());
            }
        }


        public void SaveCategory(Enums.Category category)
        {
            Category c = Kernel.Database.Category.Find(q => q.Naam == category.ToString()).FirstOrDefault();
            if (c != null)
            {
                _currentTransaction.CategoryID = c.CategoryID;
                Kernel.Database.Commit();
            }
            Model.Transaction.removeEqualTransactions(ref _transactions, _currentTransaction);


        }
    }
}
