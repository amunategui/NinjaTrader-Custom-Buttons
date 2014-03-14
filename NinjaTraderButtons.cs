
#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using System.Windows.Forms;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Indicator;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Strategy;
#endregion

namespace NinjaTrader.Strategy
{
    public class TraderCompact : Strategy
    {
       #region Variables
                        private IOrder Entry1 = null;
                        int _AddTicks = 3;
                        private int dollarAtRisk = 25;
                        private int newDollarsAtRisk = 0;
                        private double newStopPrice = 0;
       #endregion

                protected override void Initialize()
        {
                CalculateOnBarClose = false;
                        newDollarsAtRisk = dollarAtRisk;
            EntriesPerDirection = 10;
            EntryHandling = EntryHandling.AllEntries;
            RealtimeErrorHandling = RealtimeErrorHandling.TakeNoAction;
        }

                protected override void OnStartUp()
                {
                         CreateForm();
             Reset();
                }

                private int _LastCurrentBar = 0;
                protected override void OnBarUpdate()
                {
               if (Historical)
                       return;

               if (CurrentBar < 5)
                       return;

                                if (_LastCurrentBar != CurrentBar)
                                {
                                        UpdateNumberData(1);
                                        ClearBoldFont();
                                        label_Size1.Font = new System.Drawing.Font("Microsoft Sans
Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                }

                                _LastCurrentBar = CurrentBar;
        }

                protected override void OnMarketData(MarketDataEventArgs e)
        {
                        UpdateRiskData();
                }

                #region Form
                //Form
                Form frm = null;
                private System.Windows.Forms.Button button_LONG;
                private System.Windows.Forms.Button button_SHORT;
                private System.Windows.Forms.TextBox textBox_Size;
                private System.Windows.Forms.TextBox textBox_Risk;
                private System.Windows.Forms.Label label_Size5;
                private System.Windows.Forms.Label label_Size4;
                private System.Windows.Forms.Label label_Size3;
                private System.Windows.Forms.Label label_Size1;
                private System.Windows.Forms.Label label_Size0;
                private System.Windows.Forms.Label label_Size2;
                private System.Windows.Forms.Label label1;
                private System.Windows.Forms.Label label2;


                private void CreateForm()
        {
                        if (ChartControl != null)
                        {
                                this.button_LONG = new System.Windows.Forms.Button();
                                this.button_SHORT = new System.Windows.Forms.Button();
                                this.textBox_Risk = new System.Windows.Forms.TextBox();
                                this.textBox_Size = new System.Windows.Forms.TextBox();
                                this.label_Size5 = new System.Windows.Forms.Label();
                                this.label_Size4 = new System.Windows.Forms.Label();
                                this.label_Size3 = new System.Windows.Forms.Label();
                                this.label_Size1 = new System.Windows.Forms.Label();
                                this.label_Size0 = new System.Windows.Forms.Label();
                                this.label_Size2 = new System.Windows.Forms.Label();
                                this.label1 = new System.Windows.Forms.Label();
                                this.label2 = new System.Windows.Forms.Label();
                                //
                                // button_LONG_1
                                //
                                this.button_LONG.BackColor = System.Drawing.SystemColors.ActiveCaption;
                                this.button_LONG.Location = new System.Drawing.Point(5, 31);
                                this.button_LONG.Name = "button_LONG";
                                this.button_LONG.Size = new System.Drawing.Size(189, 37);
                                this.button_LONG.TabIndex = 0;
                                this.button_LONG.Text = "BUY";
                                this.button_LONG.UseVisualStyleBackColor = false;
                                this.button_LONG.Click += new System.EventHandler(this.button_LONG_Click);

                                //
                                // button_SHORT_1
                                //
                                this.button_SHORT.BackColor = System.Drawing.Color.Tomato;
                                this.button_SHORT.Location = new System.Drawing.Point(5, 74);
                                this.button_SHORT.Name = "button_SHORT";
                                this.button_SHORT.Size = new System.Drawing.Size(189, 37);
                                this.button_SHORT.TabIndex = 1;
                                this.button_SHORT.Text = "SELL";
                                this.button_SHORT.UseVisualStyleBackColor = false;
                                this.button_SHORT.Click += new System.EventHandler(this.button_SHORT_Click);
                                //
                                // textBox_Risk
                                //
                                this.textBox_Risk.Location = new System.Drawing.Point(21, 125);
                                this.textBox_Risk.Name = "textBox_Size";
                                this.textBox_Risk.Size = new System.Drawing.Size(69, 20);
                                this.textBox_Risk.TabIndex = 2;
                                //this.textBox_Risk.TextChanged += new
                                System.EventHandler(this.textBox_Risk_TextChanged);
                                this.textBox_Risk.KeyUp += new
                                System.Windows.Forms.KeyEventHandler(this.PriceChange);
                                //
                                // textBox_Size
                                //
                                this.textBox_Size.Location = new System.Drawing.Point(116, 125);
                                this.textBox_Size.Name = "textBox_Size";
                                this.textBox_Size.Size = new System.Drawing.Size(78, 20);
                                this.textBox_Size.TabIndex = 3;
                                //
                                // label_Size5
                                //
                                this.label_Size5.AutoSize = true;
                                this.label_Size5.Location = new System.Drawing.Point(7, 9);
                                this.label_Size5.Name = "label_Size5";
                                this.label_Size5.Tag = 5;
                                this.label_Size5.Size = new System.Drawing.Size(31, 13);
                                this.label_Size5.TabIndex = 4;
                                this.label_Size5.Text = "8888";
                                this.label_Size5.Click += new System.EventHandler(this.label_Size5_Click);
                                //
                                // label_Size4
                                //
                                this.label_Size4.AutoSize = true;
                                this.label_Size4.Location = new System.Drawing.Point(38, 9);
                                this.label_Size4.Name = "label_Size4";
                                this.label_Size4.Size = new System.Drawing.Size(31, 13);
                                this.label_Size4.TabIndex = 5;
                                this.label_Size5.Tag = 4;
                                this.label_Size4.Text = "8888";
                                this.label_Size4.Click += new System.EventHandler(this.label_Size4_Click);
                                //
                                // label_Size3
                                //
                                this.label_Size3.AutoSize = true;
                                this.label_Size3.Location = new System.Drawing.Point(66, 9);
                                this.label_Size3.Name = "label_Size3";
                                this.label_Size3.Size = new System.Drawing.Size(31, 13);
                                this.label_Size3.TabIndex = 6;
                                this.label_Size5.Tag = 3;
                                this.label_Size3.Text = "8888";
                                this.label_Size3.Click += new System.EventHandler(this.label_Size3_Click);
                                //
                                // label_Size2
                                //
                                this.label_Size2.AutoSize = true;
                                this.label_Size2.Location = new System.Drawing.Point(96, 9);
                                this.label_Size2.Name = "label_Size2";
                                this.label_Size2.Size = new System.Drawing.Size(31, 13);
                                this.label_Size2.TabIndex = 9;
                                this.label_Size5.Tag = 2;
                                this.label_Size2.Text = "8888";
                                this.label_Size2.Click += new System.EventHandler(this.label_Size2_Click);
                                //
                                // label_Size1
                                //
                                this.label_Size1.AutoSize = true;
                                this.label_Size1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                this.label_Size1.Location = new System.Drawing.Point(127, 9);
                                this.label_Size1.Name = "label_Size1";
                                this.label_Size1.Size = new System.Drawing.Size(35, 13);
                                this.label_Size1.TabIndex = 7;
                                this.label_Size1.Text = "8888";
                                this.label_Size5.Tag = 1;
                                this.label_Size1.Click += new System.EventHandler(this.label_Size1_Click);
                                //
                                // label_Size0
                                //
                                this.label_Size0.AutoSize = true;
                                this.label_Size0.Location = new System.Drawing.Point(164, 9);
                                this.label_Size0.Name = "label_Size0";
                                this.label_Size0.Size = new System.Drawing.Size(31, 13);
                                this.label_Size0.TabIndex = 8;
                                this.label_Size5.Tag = 0;
                                this.label_Size0.Text = "8888";
                                this.label_Size0.Click += new System.EventHandler(this.label_Size0_Click);
                                //
                                // label1
                                //
                                this.label1.AutoSize = true;
                                this.label1.Location = new System.Drawing.Point(5, 127);
                                this.label1.Name = "label1";
                                this.label1.Size = new System.Drawing.Size(13, 13);
                                this.label1.TabIndex = 5;
                                this.label1.Text = "$";
                                //
                                // label2
                                //
                                this.label2.AutoSize = true;
                                this.label2.Location = new System.Drawing.Point(96, 128);
                                this.label2.Name = "label2";
                                this.label2.Size = new System.Drawing.Size(14, 13);
                                this.label2.TabIndex = 6;
                                this.label2.Text = "S";
                                //
                                // Form1
                                //
                                frm = new Form();
                                frm.FormClosing += new
                                System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
                                frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                                frm.Name = "ChartTrader";
                                frm.Text = this.Instrument.FullName + "-" + Bars.Period.Value.ToString() + "-"
                                + this.Account.Name.ToString();
                                frm.Controls.Add(this.button_LONG);
                                frm.Controls.Add(this.button_SHORT);
                                frm.Controls.Add(this.label2);
                                frm.Controls.Add(this.label1);
                                frm.Controls.Add(this.label_Size5);
                                frm.Controls.Add(this.label_Size4);
                                frm.Controls.Add(this.label_Size3);
                                frm.Controls.Add(this.label_Size2);
                                frm.Controls.Add(this.label_Size1);
                                frm.Controls.Add(this.label_Size0);
                                frm.Controls.Add(this.textBox_Size);
                                frm.Controls.Add(this.textBox_Risk);
                                frm.TopMost = true;
                                frm.Size = new System.Drawing.Size(220, 185);
                                frm.Show();
                        }
                }

        endregion

        #region Events

                double quantityToRisk0 = 0;
                double quantityToRisk1 = 0;
                double quantityToRisk2 = 0;
                double quantityToRisk3 = 0;
                double quantityToRisk4 = 0;
                double quantityToRisk5 = 0;
                private void UpdateRiskData()
                {

                        quantityToRisk0 = Math.Round(dollarAtRisk  / ((High[0] - Low[0]) + (TickSize * 2)),0);
                        quantityToRisk1 = Math.Round(dollarAtRisk  / ((High[1] - Low[1]) + (TickSize * 2)),0);
                        quantityToRisk2 = Math.Round(dollarAtRisk  / ((High[2] - Low[2]) + (TickSize * 2)),0);
                        quantityToRisk3 = Math.Round(dollarAtRisk  / ((High[3] - Low[3]) + (TickSize * 2)),0);
                        quantityToRisk4 = Math.Round(dollarAtRisk  / ((High[4] - Low[4]) + (TickSize * 2)),0);
                        quantityToRisk5 = Math.Round(dollarAtRisk  / ((High[5] - Low[5]) + (TickSize * 2)),0);


                        frm.Text = this.Instrument.FullName + "-" + Bars.Period.Value.ToString() + "-"
                                                + this.Account.Name.ToString() + " (" + (quantityToRisk1 * Close[0]) + ")";

                        label_Size0.Text = Convert.ToString(quantityToRisk0);
                        label_Size1.Text = Convert.ToString(quantityToRisk1);
                        label_Size2.Text = Convert.ToString(quantityToRisk2);
                        label_Size3.Text = Convert.ToString(quantityToRisk3);
                        label_Size4.Text = Convert.ToString(quantityToRisk4);
                        label_Size5.Text = Convert.ToString(quantityToRisk5);
                }

                double selectedHigh = 0;
                double selectedLow = 0;
                private void UpdateNumberData(int barIndex)
                {
                        selectedHigh = High[barIndex];
                        selectedLow = Low[barIndex];
                        button_LONG.Text = Convert.ToString(selectedHigh);
                        button_SHORT.Text = Convert.ToString(selectedLow);


                        //Check for dollars at risk
                        if (textBox_Risk.Text != "")
                        {
                                bool isNumber = int.TryParse(textBox_Risk.Text, out newDollarsAtRisk);
                                 if (!isNumber)
                                        newDollarsAtRisk = dollarAtRisk;
                                else
                                        dollarAtRisk = newDollarsAtRisk;

                                textBox_Risk.Text = newDollarsAtRisk.ToString();
                        }
                        else
                        {
                                textBox_Risk.Text = dollarAtRisk.ToString();
                        }

                        textBox_Size.Text = Convert.ToString( Math.Round(dollarAtRisk  / ((selectedHigh - selectedLow) + (TickSize * 2)),0));

                }

       protected override void OnTermination()
       {
                        if (ChartControl != null)
                        {
                                if (frm != null)
                                {
                                        //remove the buttons
                                        this.button_LONG.Click -= new System.EventHandler(button_LONG_Click);
                                        frm.Controls.Remove(button_LONG);
                                        this.button_LONG.Dispose();

                                        this.button_SHORT.Click -= new System.EventHandler(button_SHORT_Click);
                                        frm.Controls.Remove(button_SHORT);
                                        this.button_SHORT.Dispose();

                                        frm.Controls.Remove(textBox_Size);
                                        textBox_Size.Dispose();

                                        frm.Controls.Remove(textBox_Risk);
                                        textBox_Risk.Dispose();

                                        frm.Controls.Remove(label2);
                                        label2.Dispose();
                                        frm.Controls.Remove(label1);
                                        label1.Dispose();
                                        frm.Controls.Remove(label_Size5);
                                        label_Size5.Dispose();
                                        frm.Controls.Remove(label_Size4);
                                        label_Size3.Dispose();
                                        frm.Controls.Remove(label_Size3);
                                        label_Size3.Dispose();
                                        frm.Controls.Remove(label_Size2);
                                        label_Size2.Dispose();
                                        frm.Controls.Remove(label_Size1);
                                        label_Size1.Dispose();
                                        frm.Controls.Remove(label_Size0);
                                        label_Size0.Dispose();

                                        frm.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
                                        frm.Close();
                                        frm.Dispose();
                                }
                        }
       }


                private void PriceChange(object sender, KeyEventArgs e)
        {
                        UpdateRiskData();
                        UpdateNumberData(1);
                        ClearBoldFont();
                        label_Size1.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

                private void label_Size5_Click(object sender, EventArgs e)
                {
                        UpdateNumberData(5);
                        ClearBoldFont();
                        label_Size5.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                private void label_Size4_Click(object sender, EventArgs e)
                {
                        UpdateNumberData(4);
                        ClearBoldFont();
                        label_Size4.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                private void label_Size3_Click(object sender, EventArgs e)
                {
                        UpdateNumberData(3);
                        ClearBoldFont();
                        label_Size3.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                private void label_Size2_Click(object sender, EventArgs e)
                {
                        UpdateNumberData(2);
                        ClearBoldFont();
                        label_Size2.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                private void label_Size1_Click(object sender, EventArgs e)
                {
                        UpdateNumberData(1);
                        ClearBoldFont();
                        label_Size1.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                private void label_Size0_Click(object sender, EventArgs e)
                {
                        UpdateNumberData(0);
                        ClearBoldFont();
                        label_Size0.Font = new System.Drawing.Font("Microsoft Sans Serif",
8.25F, System.Drawing.FontStyle.Bold,
System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

       private void button_LONG_Click(object sender, EventArgs e)
       {
            TriggerCustomEvent(new CustomEvent(button_LONG_Event),0,e);
       }

       private void button_LONG_Event(object state)
       {
            Trade(true, int.Parse(textBox_Size.Text), selectedHigh,
selectedHigh, selectedLow);
       }

       private void button_SHORT_Click(object sender, EventArgs e)
       {
                        TriggerCustomEvent(new CustomEvent(button_SHORT_Event),0,e);
       }

       private void button_SHORT_Event(object state)
       {
            Trade(false, int.Parse(textBox_Size.Text), selectedLow,
selectedHigh, selectedLow);
       }



                private void frm_FormClosing(object sender,FormClosingEventArgs e)
                {
                        TriggerCustomEvent(new CustomEvent(frmFormClosing),0,e);
                }

                private void frmFormClosing(object state)
                {
                        FormClosingEventArgs e = (FormClosingEventArgs)state;
                        if (e.CloseReason == CloseReason.UserClosing)
                        {
                                        e.Cancel = true;
                        }
                }

        #endregion

        #region Trading

                                        int PositionCounter = 0;
                                        private void Trade(bool GoingLong, int TheSize, double ThePrice,
double TheHigh, double TheLow)
                                        {
                                                if (Entry1 != null)
                                                        return;

                                                PositionCounter += 1;
                                                double myhigh =(((TheHigh - TheLow) +TheHigh) + (TickSize * _AddTicks));
                                                double mylow = ((TheLow - (TheHigh - TheLow)) - (TickSize * _AddTicks));

                                                                        if (GoingLong == true)
                                                                        {
                                                                                        ThePrice += (TickSize * 1);
                                                                                        SetStopLoss("Entry1_" + PositionCounter,
CalculationMode.Price, TheLow - (TickSize * 2), false);
                                                                                        SetProfitTarget("Entry1_" + PositionCounter,
CalculationMode.Price, myhigh);

                                                                                        if (ThePrice > Close[0])
                                                                                                Entry1 = EnterLongStop (TheSize, ThePrice, "Entry1_" +
PositionCounter);
                                                                                        else
                                                                                                Entry1 = EnterLongLimit (TheSize, ThePrice, "Entry1_" +
PositionCounter);
                                                                        }
                                                                        else
                                                                        {
                                                                                        ThePrice -= (TickSize * 1);

                                                                                        SetStopLoss("Entry1_" + PositionCounter,
CalculationMode.Price, TheHigh + (TickSize * 2), false);
                                                                                        SetProfitTarget("Entry1_" + PositionCounter,
CalculationMode.Price, mylow);

                                                                                        if (ThePrice < Close[0])
                                                                                        {
                                                                                                        Entry1 = EnterShortStop (TheSize, ThePrice, "Entry1_" +
PositionCounter);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                                        Entry1 = EnterShortLimit (TheSize, ThePrice, "Entry1_" +
PositionCounter);
                                                                                        }
                                                                        }
                                                        }


                                        protected override void OnOrderUpdate(IOrder order)
                    {


                                                if (order.Name == "Profit target" && order.OrderState == OrderState.PendingChange)
                                                {
                                                        double newPrice = order.LimitPrice;
                                                         SetProfitTarget("Entry1_" + PositionCounter,
CalculationMode.Price, newPrice);
                                                }

                                                if (order.Name == "Stop loss" && order.OrderState ==
OrderState.PendingChange)
                                                {
                                                        double newPrice = order.StopPrice;
                                                        SetTrailStop(order.StopPrice,false);
                                                        SetStopLoss("Entry1_" + PositionCounter, CalculationMode.Price,
newPrice, false);
                                                        SetTrailStop("Entry1_" + PositionCounter,
CalculationMode.Price, newPrice, false);
                                                }

                                                if (order.OrderState == OrderState.Cancelled)
                        {
                                                        Print("order.OrderState == OrderState.Cancelled");
                            Reset();
                                                }
                       if (order.OrderState == OrderState.Rejected)
                       {
                               Print("order.OrderState == OrderState.Rejected");
                               // Stop loss order was rejected !!!!
                               // Do something about it here
                       }
                    }


        #endregion

        #region Utilities

                private void ClearBoldFont()
                {
                    this.label_Size0.Font = new System.Drawing.Font("Microsoft Sans
                    Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.label_Size1.Font = new System.Drawing.Font("Microsoft Sans
                    Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.label_Size2.Font = new System.Drawing.Font("Microsoft Sans
                    Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.label_Size3.Font = new System.Drawing.Font("Microsoft Sans
                    Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.label_Size4.Font = new System.Drawing.Font("Microsoft Sans
                    Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.label_Size5.Font = new System.Drawing.Font("Microsoft Sans
                    Serif", 8.25F, System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }

                private void Reset()
                {
                        Entry1 = null;
                }

                private int GetTradeSize(double TheHigh, double TheLow)
                {
                        int quantityToRisk = (int) Math.Round(dollarAtRisk  / ((TheHigh - TheLow) + (TickSize * 2)),0);
                        //if(quantityToRisk % 2 != 0)
                        //      quantityToRisk += 1;
                        return quantityToRisk;
                }

            #endregion

            #region Properties

               [Description("Max dollars at risk")]
               [GridCategory("Parameters")]
               public int DollarAtRisk
               {
                       get { return dollarAtRisk; }
                       set { dollarAtRisk = Math.Max(1, value); }
               }

               #endregion

        }
}