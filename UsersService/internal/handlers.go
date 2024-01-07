package internal

import (
	"encoding/json"
	"net/http"
)

func UsersHandler(w http.ResponseWriter, r *http.Request) {
	users, err := generateRandomUsers(10)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	respondWithJSON(w, users, http.StatusOK)
}

func UserHandler(w http.ResponseWriter, r *http.Request) {
	user, err := generateRandomUser()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	respondWithJSON(w, user, http.StatusOK)
}

func respondWithJSON(w http.ResponseWriter, data interface{}, statusCode int) {
	jsonResponse, err := json.Marshal(data)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(statusCode)
	w.Write(jsonResponse)
}
