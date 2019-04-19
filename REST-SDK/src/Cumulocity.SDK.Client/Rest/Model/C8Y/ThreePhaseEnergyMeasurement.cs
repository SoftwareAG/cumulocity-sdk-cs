using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Measurement;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_ThreePhaseEnergyMeasurement")]
    public class ThreePhaseEnergyMeasurement : SinglePhaseEnergyMeasurement
    {

        private MeasurementValue Ri_plus, Ri_minus, Rc_plus, Rc_minus;

        private MeasurementValue Qi_plus, Qi_minus, Qc_plus, Qc_minus;

        private MeasurementValue A_plus_phase1, A_minus_phase1, A_plus_phase2, A_minus_phase2, A_plus_phase3, A_minus_phase3;

        private MeasurementValue P_plus_phase1, P_minus_phase1, P_plus_phase2, P_minus_phase2, P_plus_phase3, P_minus_phase3;

        public ThreePhaseEnergyMeasurement()
        {
        }

        public ThreePhaseEnergyMeasurement(MeasurementValue a_plus, MeasurementValue a_minus, MeasurementValue p_plus, MeasurementValue p_minus, MeasurementValue ri_plus, MeasurementValue ri_minus, MeasurementValue rc_plus, MeasurementValue rc_minus, MeasurementValue qi_plus, MeasurementValue qi_minus, MeasurementValue qc_plus, MeasurementValue qc_minus, MeasurementValue a_plus_phase1, MeasurementValue a_minus_phase1, MeasurementValue a_plus_phase2, MeasurementValue a_minus_phase2, MeasurementValue a_plus_phase3, MeasurementValue a_minus_phase3, MeasurementValue p_plus_phase1, MeasurementValue p_minus_phase1, MeasurementValue p_plus_phase2, MeasurementValue p_minus_phase2, MeasurementValue p_plus_phase3, MeasurementValue p_minus_phase3) : base(a_plus, a_minus, p_plus, p_minus)
        {
            Ri_plus = ri_plus;
            Ri_minus = ri_minus;
            Rc_plus = rc_plus;
            Rc_minus = rc_minus;
            Qi_plus = qi_plus;
            Qi_minus = qi_minus;
            Qc_plus = qc_plus;
            Qc_minus = qc_minus;
            A_plus_phase1 = a_plus_phase1;
            A_minus_phase1 = a_minus_phase1;
            A_plus_phase2 = a_plus_phase2;
            A_minus_phase2 = a_minus_phase2;
            A_plus_phase3 = a_plus_phase3;
            A_minus_phase3 = a_minus_phase3;
            P_plus_phase1 = p_plus_phase1;
            P_minus_phase1 = p_minus_phase1;
            P_plus_phase2 = p_plus_phase2;
            P_minus_phase2 = p_minus_phase2;
            P_plus_phase3 = p_plus_phase3;
            P_minus_phase3 = p_minus_phase3;
        }

        /// <returns> the Ri_plus </returns>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "")]
        //ORIGINAL LINE: @JSONProperty(value = "Ri+", ignoreIfNull = true) public final MeasurementValue getTotalReactiveInductiveEnergyIn()
        public MeasurementValue TotalReactiveInductiveEnergyIn
        {
            get
            {
                return Ri_plus;
            }
            set
            {
                Ri_plus = value;
            }
        }


        /// <returns> the Ri_minus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Ri-", ignoreIfNull = true) public final MeasurementValue getTotalReactiveInductiveEnergyOut()
        public MeasurementValue TotalReactiveInductiveEnergyOut
        {
            get
            {
                return Ri_minus;
            }
            set
            {
                Ri_minus = value;
            }
        }


        /// <returns> the Rc_plus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Rc+", ignoreIfNull = true) public final MeasurementValue getTotalReactiveCapacitiveEnergyIn()
        public MeasurementValue TotalReactiveCapacitiveEnergyIn
        {
            get
            {
                return Rc_plus;
            }
            set
            {
                Rc_plus = value;
            }
        }


        /// <returns> the Rc_minus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Rc-", ignoreIfNull = true) public final MeasurementValue getTotalReactiveCapacitiveEnergyOut()
        public MeasurementValue TotalReactiveCapacitiveEnergyOut
        {
            get
            {
                return Rc_minus;
            }
            set
            {
                Rc_minus = value;
            }
        }


        /// <returns> the qi_plus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Qi+", ignoreIfNull = true) public final MeasurementValue getTotalReactiveInductivePowerIn()
        public MeasurementValue TotalReactiveInductivePowerIn
        {
            get
            {
                return Qi_plus;
            }
            set
            {
                Qi_plus = value;
            }
        }


        /// <returns> the qi_minus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Qi-", ignoreIfNull = true) public final MeasurementValue getTotalReactiveInductivePowerOut()
        public MeasurementValue TotalReactiveInductivePowerOut
        {
            get
            {
                return Qi_minus;
            }
            set
            {
                Qi_minus = value;
            }
        }


        /// <returns> the qc_plus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Qc+", ignoreIfNull = true) public final MeasurementValue getTotalReactiveCapacitivePowerIn()
        public MeasurementValue TotalReactiveCapacitivePowerIn
        {
            get
            {
                return Qc_plus;
            }
            set
            {
                Qc_plus = value;
            }
        }


        /// <returns> the qc_minus </returns>
        //ORIGINAL LINE: @JSONProperty(value = "Qc-", ignoreIfNull = true) public final MeasurementValue getTotalReactiveCapacitivePowerOut()
        public MeasurementValue TotalReactiveCapacitivePowerOut
        {
            get
            {
                return Qc_minus;
            }
            set
            {
                Qc_minus = value;
            }
        }


        /// <returns> the a_plus_phase1 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "A+:1", ignoreIfNull = true) public final MeasurementValue getTotalActiveEnergyInPhase1()
        public MeasurementValue TotalActiveEnergyInPhase1
        {
            get
            {
                return A_plus_phase1;
            }
            set
            {
                A_plus_phase1 = value;
            }
        }


        /// <returns> the a_minus_phase1 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "A-:1", ignoreIfNull = true) public final MeasurementValue getTotalActiveEnergyOutPhase1()
        public MeasurementValue TotalActiveEnergyOutPhase1
        {
            get
            {
                return A_minus_phase1;
            }
            set
            {
                A_minus_phase1 = value;
            }
        }


        /// <returns> the p_plus_phase1 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P+:1", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerInPhase1()
        public MeasurementValue TotalActivePowerInPhase1
        {
            get
            {
                return P_plus_phase1;
            }
            set
            {
                P_plus_phase1 = value;
            }
        }


        /// <returns> the p_minus_phase1 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P-:1", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerOutPhase1()
        public MeasurementValue TotalActivePowerOutPhase1
        {
            get
            {
                return P_minus_phase1;
            }
            set
            {
                P_minus_phase1 = value;
            }
        }


        /// <returns> the a_plus_phase1 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "A+:2", ignoreIfNull = true) public final MeasurementValue getTotalActiveEnergyInPhase2()
        public MeasurementValue TotalActiveEnergyInPhase2
        {
            get
            {
                return A_plus_phase2;
            }
            set
            {
                A_plus_phase2 = value;
            }
        }


        /// <returns> the a_minus_phase2 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "A-:2", ignoreIfNull = true) public final MeasurementValue getTotalActiveEnergyOutPhase2()
        public MeasurementValue TotalActiveEnergyOutPhase2
        {
            get
            {
                return A_minus_phase2;
            }
            set
            {
                A_minus_phase2 = value;
            }
        }


        /// <returns> the p_plus_phase2 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P+:2", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerInPhase2()
        public MeasurementValue TotalActivePowerInPhase2
        {
            get
            {
                return P_plus_phase2;
            }
            set
            {
                P_plus_phase2 = value;
            }
        }


        /// <returns> the p_minus_phase2 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P-:2", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerOutPhase2()
        public MeasurementValue TotalActivePowerOutPhase2
        {
            get
            {
                return P_minus_phase2;
            }
            set
            {
                P_minus_phase2 = value;
            }
        }


        /// <returns> the a_plus_phase3 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "A+:3", ignoreIfNull = true) public final MeasurementValue getTotalActiveEnergyInPhase3()
        public MeasurementValue TotalActiveEnergyInPhase3
        {
            get
            {
                return A_plus_phase3;
            }
            set
            {
                A_plus_phase3 = value;
            }
        }


        /// <returns> the a_minus_phase3 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "A-:3", ignoreIfNull = true) public final MeasurementValue getTotalActiveEnergyOutPhase3()
        public MeasurementValue TotalActiveEnergyOutPhase3
        {
            get
            {
                return A_minus_phase3;
            }
            set
            {
                A_minus_phase3 = value;
            }
        }


        /// <returns> the p_plus_phase3 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P+:3", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerInPhase3()
        public MeasurementValue TotalActivePowerInPhase3
        {
            get
            {
                return P_plus_phase3;
            }
            set
            {
                P_plus_phase3 = value;
            }
        }


        /// <returns> the p_minus_phase3 </returns>
        //ORIGINAL LINE: @JSONProperty(value = "P-:3", ignoreIfNull = true) public final MeasurementValue getTotalActivePowerOutPhase3()
        public MeasurementValue TotalActivePowerOutPhase3
        {
            get
            {
                return P_minus_phase3;
            }
            set
            {
                P_minus_phase3 = value;
            }
        }


        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            result = 31 * result + (Ri_plus != null ? Ri_plus.GetHashCode() : 0);
            result = 31 * result + (Ri_minus != null ? Ri_minus.GetHashCode() : 0);
            result = 31 * result + (Rc_plus != null ? Rc_plus.GetHashCode() : 0);
            result = 31 * result + (Rc_minus != null ? Rc_minus.GetHashCode() : 0);
            result = 31 * result + (Qi_plus != null ? Qi_plus.GetHashCode() : 0);
            result = 31 * result + (Qi_minus != null ? Qi_minus.GetHashCode() : 0);
            result = 31 * result + (Qc_plus != null ? Qc_plus.GetHashCode() : 0);
            result = 31 * result + (Qc_minus != null ? Qc_minus.GetHashCode() : 0);
            result = 31 * result + (A_plus_phase1 != null ? A_plus_phase1.GetHashCode() : 0);
            result = 31 * result + (A_minus_phase1 != null ? A_minus_phase1.GetHashCode() : 0);
            result = 31 * result + (A_plus_phase2 != null ? A_plus_phase2.GetHashCode() : 0);
            result = 31 * result + (A_minus_phase2 != null ? A_minus_phase2.GetHashCode() : 0);
            result = 31 * result + (A_plus_phase3 != null ? A_plus_phase3.GetHashCode() : 0);
            result = 31 * result + (A_minus_phase3 != null ? A_minus_phase3.GetHashCode() : 0);
            result = 31 * result + (P_plus_phase1 != null ? P_plus_phase1.GetHashCode() : 0);
            result = 31 * result + (P_minus_phase1 != null ? P_minus_phase1.GetHashCode() : 0);
            result = 31 * result + (P_plus_phase2 != null ? P_plus_phase2.GetHashCode() : 0);
            result = 31 * result + (P_minus_phase2 != null ? P_minus_phase2.GetHashCode() : 0);
            result = 31 * result + (P_plus_phase3 != null ? P_plus_phase3.GetHashCode() : 0);
            result = 31 * result + (P_minus_phase3 != null ? P_minus_phase3.GetHashCode() : 0);
            return result;
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (!(o is ThreePhaseEnergyMeasurement))
            {
                return false;
            }
            if (!base.Equals(o))
            {
                return false;
            }

            ThreePhaseEnergyMeasurement that = (ThreePhaseEnergyMeasurement)o;

            if (A_minus_phase1 != null ? !A_minus_phase1.Equals(that.A_minus_phase1) : that.A_minus_phase1 != null)
            {
                return false;
            }
            if (A_minus_phase2 != null ? !A_minus_phase2.Equals(that.A_minus_phase2) : that.A_minus_phase2 != null)
            {
                return false;
            }
            if (A_minus_phase3 != null ? !A_minus_phase3.Equals(that.A_minus_phase3) : that.A_minus_phase3 != null)
            {
                return false;
            }
            if (A_plus_phase1 != null ? !A_plus_phase1.Equals(that.A_plus_phase1) : that.A_plus_phase1 != null)
            {
                return false;
            }
            if (A_plus_phase2 != null ? !A_plus_phase2.Equals(that.A_plus_phase2) : that.A_plus_phase2 != null)
            {
                return false;
            }
            if (A_plus_phase3 != null ? !A_plus_phase3.Equals(that.A_plus_phase3) : that.A_plus_phase3 != null)
            {
                return false;
            }
            if (P_minus_phase1 != null ? !P_minus_phase1.Equals(that.P_minus_phase1) : that.P_minus_phase1 != null)
            {
                return false;
            }
            if (P_minus_phase2 != null ? !P_minus_phase2.Equals(that.P_minus_phase2) : that.P_minus_phase2 != null)
            {
                return false;
            }
            if (P_minus_phase3 != null ? !P_minus_phase3.Equals(that.P_minus_phase3) : that.P_minus_phase3 != null)
            {
                return false;
            }
            if (P_plus_phase1 != null ? !P_plus_phase1.Equals(that.P_plus_phase1) : that.P_plus_phase1 != null)
            {
                return false;
            }
            if (P_plus_phase2 != null ? !P_plus_phase2.Equals(that.P_plus_phase2) : that.P_plus_phase2 != null)
            {
                return false;
            }
            if (P_plus_phase3 != null ? !P_plus_phase3.Equals(that.P_plus_phase3) : that.P_plus_phase3 != null)
            {
                return false;
            }
            if (Qc_minus != null ? !Qc_minus.Equals(that.Qc_minus) : that.Qc_minus != null)
            {
                return false;
            }
            if (Qc_plus != null ? !Qc_plus.Equals(that.Qc_plus) : that.Qc_plus != null)
            {
                return false;
            }
            if (Qi_minus != null ? !Qi_minus.Equals(that.Qi_minus) : that.Qi_minus != null)
            {
                return false;
            }
            if (Qi_plus != null ? !Qi_plus.Equals(that.Qi_plus) : that.Qi_plus != null)
            {
                return false;
            }
            if (Rc_minus != null ? !Rc_minus.Equals(that.Rc_minus) : that.Rc_minus != null)
            {
                return false;
            }
            if (Rc_plus != null ? !Rc_plus.Equals(that.Rc_plus) : that.Rc_plus != null)
            {
                return false;
            }
            if (Ri_minus != null ? !Ri_minus.Equals(that.Ri_minus) : that.Ri_minus != null)
            {
                return false;
            }
            if (Ri_plus != null ? !Ri_plus.Equals(that.Ri_plus) : that.Ri_plus != null)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return "ThreePhaseEnergyMeasurement{" +
                    "Ri_plus=" + Ri_plus +
                    ", Ri_minus=" + Ri_minus +
                    ", Rc_plus=" + Rc_plus +
                    ", Rc_minus=" + Rc_minus +
                    ", Qi_plus=" + Qi_plus +
                    ", Qi_minus=" + Qi_minus +
                    ", Qc_plus=" + Qc_plus +
                    ", Qc_minus=" + Qc_minus +
                    ", A_plus_phase1=" + A_plus_phase1 +
                    ", A_minus_phase1=" + A_minus_phase1 +
                    ", A_plus_phase2=" + A_plus_phase2 +
                    ", A_minus_phase2=" + A_minus_phase2 +
                    ", A_plus_phase3=" + A_plus_phase3 +
                    ", A_minus_phase3=" + A_minus_phase3 +
                    ", P_plus_phase1=" + P_plus_phase1 +
                    ", P_minus_phase1=" + P_minus_phase1 +
                    ", P_plus_phase2=" + P_plus_phase2 +
                    ", P_minus_phase2=" + P_minus_phase2 +
                    ", P_plus_phase3=" + P_plus_phase3 +
                    ", P_minus_phase3=" + P_minus_phase3 +
                    '}';
        }
    }

}
