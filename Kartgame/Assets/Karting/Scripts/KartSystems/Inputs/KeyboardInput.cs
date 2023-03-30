using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";
        public string UseItemButtonName = "Use Item";

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = Input.GetButton(AccelerateButtonName),
                Brake = Input.GetButton(BrakeButtonName),
                UseItem = Input.GetButton(UseItemButtonName),
                TurnInput = Input.GetAxis("Horizontal")
            };
        }
    }
}
