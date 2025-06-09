package org.example;


import com.fasterxml.jackson.annotation.JsonProperty;

public class InfoMoeda {

    @JsonProperty("name")
    protected String name;
    @JsonProperty("current_price")
    protected double current_price;
    @JsonProperty("high_24h")
    protected double high_24h;
    @JsonProperty("low_24h")
    protected double low_24;
    @JsonProperty("total_volume")
    protected double total_volume;

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public double getCurrent_price() {
        return current_price;
    }
    public void setCurrent_price(double current_price) {
        this.current_price = current_price;
    }
    public double getHigh_24h() {
        return high_24h;
    }
    public void setHigh_24h(double high_24h) {
        this.high_24h = high_24h;
    }
    public double getLow_24() {
        return low_24;
    }
    public void setLow_24(double low_24) {
        this.low_24 = low_24;
    }
    public double getTotal_volume() {
        return total_volume;
    }
    public void setTotal_volume(double total_volume) {
        this.total_volume = total_volume;
    }
}
