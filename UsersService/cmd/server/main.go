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
	fmt.Println("Server is running on port 8080...")
	log.Fatal(http.ListenAndServe(":8080", nil))
}
