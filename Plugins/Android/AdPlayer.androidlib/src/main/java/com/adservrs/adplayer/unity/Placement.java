package com.adservrs.adplayer.unity;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.graphics.PixelFormat;
import android.util.Log;
import android.view.Gravity;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.FrameLayout;

import androidx.annotation.Keep;

import com.adservrs.adplayer.placements.AdPlayerPlacementView;
import com.unity3d.player.UnityPlayer;

import java.util.concurrent.atomic.AtomicBoolean;

@Keep
public class Placement {
    private static final String TAG = "UPlacement";

    private final AtomicBoolean disposed = new AtomicBoolean(false);

    private final Activity activity = UnityPlayer.currentActivity;
    private final AdPlayerPlacementView placementView;
    private final ViewGroup rootView;

    private final WindowManager windowManager = activity.getWindowManager();
    private final WindowManager.LayoutParams layoutParams = new WindowManager.LayoutParams(
            WindowManager.LayoutParams.TYPE_APPLICATION_PANEL,
            WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED
                    | WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE
                    | WindowManager.LayoutParams.FLAG_NOT_TOUCH_MODAL
                    | WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS
    );

    @SuppressLint("RtlHardcoded")
    public Placement() {
        placementView = new AdPlayerPlacementView(UnityPlayer.currentContext);

        rootView = new FrameLayout(UnityPlayer.currentContext);
        rootView.addView(placementView, new FrameLayout.LayoutParams(
                FrameLayout.LayoutParams.MATCH_PARENT,
                FrameLayout.LayoutParams.WRAP_CONTENT,
                Gravity.CENTER
        ));

        layoutParams.gravity = Gravity.TOP | Gravity.LEFT;
        layoutParams.width = 0;
        layoutParams.height = 0;
        layoutParams.format = PixelFormat.TRANSPARENT;

        activity.runOnUiThread(() -> windowManager.addView(rootView, layoutParams));
    }

    @Keep
    public void dispose() {
        Log.d(TAG, "dispose");

        activity.runOnUiThread(() -> {
            if (!disposed.compareAndSet(false, true)) {
                return;
            }
            windowManager.removeView(rootView);
        });
    }

    @Keep
    public void attachTag(String tagId) {
        Log.d(TAG, "attachTag: tagId = " + tagId);

        placementView.attachPlayerTag(tagId);
    }

    @Keep
    public void updatePosition(int x, int y, int width, int height) {
        Log.d(TAG, "updatePosition");

        activity.runOnUiThread(() -> {
            if (disposed.get()) {
                return;
            }

            layoutParams.x = x;
            layoutParams.y = y;
            layoutParams.width = width;
            layoutParams.height = height;

            windowManager.updateViewLayout(rootView, layoutParams);

            int[] location = new int[2];
            rootView.getLocationOnScreen(location);

            Log.d(TAG, location[0] + " x " + location[1]);
        });
    }
}
