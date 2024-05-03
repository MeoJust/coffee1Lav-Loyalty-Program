self.addEventListener('push', (event) => {
    const notifi = event.data.json().notification;

    event.waitUntil(self.registration.showNotification(notifi.title, {
            body: notifi.body,
            icon: noqli.image,
            data:{
                url: notifi.click_action
            }
    }));
});

self.addEventListener('notificationclick', (event) => {
    event.waitUntil(clients.openWindow(event.notification.data.url));
})