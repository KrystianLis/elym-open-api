package main

import (
	"fmt"
	"log"
	"net/http"

	"UsersService/internal"
)

func main() {
	http.HandleFunc("/api/users", internal.UsersHandler)
	http.HandleFunc("/api/user", internal.UserHandler)
	http.HandleFunc("/api/products", internal.ProductsHandler)
	http.HandleFunc("/api/order", internal.OrderHandler)
	http.HandleFunc("/api/transactions", internal.TransactionsHandler)
	http.HandleFunc("/api/audit-events", internal.AuditEventsHandler)
	http.HandleFunc("/api/devices", internal.DevicesHandler)
	fmt.Println("Server is running on port 8080...")
	log.Fatal(http.ListenAndServe(":8080", nil))
}
