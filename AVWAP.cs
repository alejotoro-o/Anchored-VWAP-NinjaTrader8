#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class AVWAP : Indicator
	{
		private DateTime anchorDate;
		double	iCumVolume			= 0;
		double	iCumTypicalVolume	= 0;
		double curVWAP = 0;
		double deviation = 0;
		double v2Sum = 0;
		double hl3 = 0;
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Anchored Volume Weighted Average Price";
				Name										= "AVWAP";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				AnchorYear									= 2024;
				AnchorMonth									= 4;
				AnchorDay									= 1;
				AnchorTime									= DateTime.Parse("08:30", System.Globalization.CultureInfo.InvariantCulture);
				AddPlot(new Stroke(Brushes.Purple, DashStyleHelper.Solid, 2), PlotStyle.Line, "PlotVWAP");
			}
		}

		protected override void OnBarUpdate()
		{
			
			DateTime anchorDate = new DateTime(AnchorYear, AnchorMonth, AnchorDay, AnchorTime.Hour, AnchorTime.Minute, 0);
			
			hl3 = ((High[0] + Low[0] + Close[0]) / 3);
			
			if (Time[0] >= anchorDate) {
			
				if (CurrentBar - Bars.GetBar(anchorDate) == 0)
				{
					iCumVolume = VOL()[0];
					iCumTypicalVolume = VOL()[0] * hl3;
					v2Sum = VOL()[0] * hl3 * hl3;
				}
				else
				{
					iCumVolume = iCumVolume + VOL()[0];
					iCumTypicalVolume = iCumTypicalVolume + ( VOL()[0] * hl3 );
					v2Sum = v2Sum + VOL()[0] * hl3 * hl3;
				}
				
				Value[0] = (iCumTypicalVolume / iCumVolume);
			
			}
			
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="AnchorYear", Order=1, GroupName="Parameters")]
		public int AnchorYear
		{ get; set; }
		
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="AnchorMonth", Order=2, GroupName="Parameters")]
		public int AnchorMonth
		{ get; set; }
		
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="AnchorDay", Order=3, GroupName="Parameters")]
		public int AnchorDay
		{ get; set; }

		[NinjaScriptProperty]
		[PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
		[Display(Name="AnchorTime", Order=4, GroupName="Parameters")]
		public DateTime AnchorTime
		{ get; set; }

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> AVWAPLine
		{
			get { return Values[0]; }
		}
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private AVWAP[] cacheAVWAP;
		public AVWAP AVWAP(int anchorYear, int anchorMonth, int anchorDay, DateTime anchorTime)
		{
			return AVWAP(Input, anchorYear, anchorMonth, anchorDay, anchorTime);
		}

		public AVWAP AVWAP(ISeries<double> input, int anchorYear, int anchorMonth, int anchorDay, DateTime anchorTime)
		{
			if (cacheAVWAP != null)
				for (int idx = 0; idx < cacheAVWAP.Length; idx++)
					if (cacheAVWAP[idx] != null && cacheAVWAP[idx].AnchorYear == anchorYear && cacheAVWAP[idx].AnchorMonth == anchorMonth && cacheAVWAP[idx].AnchorDay == anchorDay && cacheAVWAP[idx].AnchorTime == anchorTime && cacheAVWAP[idx].EqualsInput(input))
						return cacheAVWAP[idx];
			return CacheIndicator<AVWAP>(new AVWAP(){ AnchorYear = anchorYear, AnchorMonth = anchorMonth, AnchorDay = anchorDay, AnchorTime = anchorTime }, input, ref cacheAVWAP);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.AVWAP AVWAP(int anchorYear, int anchorMonth, int anchorDay, DateTime anchorTime)
		{
			return indicator.AVWAP(Input, anchorYear, anchorMonth, anchorDay, anchorTime);
		}

		public Indicators.AVWAP AVWAP(ISeries<double> input , int anchorYear, int anchorMonth, int anchorDay, DateTime anchorTime)
		{
			return indicator.AVWAP(input, anchorYear, anchorMonth, anchorDay, anchorTime);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.AVWAP AVWAP(int anchorYear, int anchorMonth, int anchorDay, DateTime anchorTime)
		{
			return indicator.AVWAP(Input, anchorYear, anchorMonth, anchorDay, anchorTime);
		}

		public Indicators.AVWAP AVWAP(ISeries<double> input , int anchorYear, int anchorMonth, int anchorDay, DateTime anchorTime)
		{
			return indicator.AVWAP(input, anchorYear, anchorMonth, anchorDay, anchorTime);
		}
	}
}

#endregion
