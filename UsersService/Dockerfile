FROM golang:1.21.5

WORKDIR /app

COPY go.mod go.sum ./

RUN go mod download

COPY . .

RUN CGO_ENABLED=0 GOOS=linux go build -v -o UsersService ./cmd/server

FROM alpine:latest

WORKDIR /root/

COPY --from=0 /app/UsersService .

EXPOSE 8080

CMD ["./UsersService"]
