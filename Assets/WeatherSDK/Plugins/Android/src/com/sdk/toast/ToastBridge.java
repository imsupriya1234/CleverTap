package com.sdk.toast;

import android.app.Activity;
import android.widget.Toast;

public class ToastBridge {
    // Called from Unity with reflection / JNI if you prefer:
    // com.sdk.toast.ToastBridge.showToast(currentActivity, message);
    public static void showToast(final Activity activity, final String message) {
        if (activity == null) return;
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(activity, message, Toast.LENGTH_SHORT).show();
            }
        });
    }
}
