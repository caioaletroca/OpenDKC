using System;

public sealed class KongStateMachineHelper {
    public static IPredicate ShouldIdle(KongController controller) {
        return new FunctionPredicate(() => Math.Abs(controller.HorizontalValue) < 0.001);
    }

    public static IPredicate ShouldWalk(KongController controller) {
        return new FunctionPredicate(() => Math.Abs(controller.HorizontalValue) > 0.001 && !controller.Run);
    }

    public static IPredicate ShouldRun(KongController controller) {
        return new FunctionPredicate(() => Math.Abs(controller.HorizontalValue) > 0.001 && controller.Run);
    }

    public static IPredicate ShouldCrouch(KongController controller) {
        return new FunctionPredicate(() => controller.VerticalValue < -0.5);
    }

    public static IPredicate ShouldJump(KongController controller) {
        return new FunctionPredicate(() => controller.Jump);
    }
}