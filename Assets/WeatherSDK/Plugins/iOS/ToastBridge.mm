#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern "C" void _ShowiOSToast(const char* msg)
{
    @autoreleasepool {
        NSString* message = msg ? [NSString stringWithUTF8String:msg] : @"";
        dispatch_async(dispatch_get_main_queue(), ^{
            UIViewController *root = UIApplication.sharedApplication.keyWindow.rootViewController;
            if (!root) return;
            UIAlertController* alert = [UIAlertController alertControllerWithTitle:nil
                                                                           message:message
                                                                    preferredStyle:UIAlertControllerStyleAlert];
            [root presentViewController:alert animated:YES completion:^{
                double delayInSeconds = 1.5;
                dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(delayInSeconds * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
                    [alert dismissViewControllerAnimated:YES completion:nil];
                });
            }];
        });
    }
}
