import React, { Component } from 'react';
import { useEffect } from 'react'
import './index.css';




export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {backgrounds: {}, forecasts: {}, loading: true };
        
    }

    async populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }


    componentDidMount() {
        this.populateWeatherData();
    }


    renderForecastsTable(forecasts) {
       
        return (
            <div className="container">
                <div className="heading">
                    <div className="content">
                    <p id="location">{forecasts[0].location}</p>
                    <p id="temp">{`${forecasts[0].temperatureF}\u00B0`}</p>
                    <p id="condition">{forecasts[0].condition}</p>
                    <p id="time">{forecasts[0].date}</p>
                    </div>
                </div>
            </div>
            );
        
        
    }

    render() {
        //makes the content load only when the promise is complete
        let contents = this.state.loading ? <p>Loading</p>: this.renderForecastsTable(this.state.forecasts);
        return (
            
            
            <div>
                <video id="background" muted preload onLoad loop autoPlay><source src='https://joy1.videvo.net/videvo_files/video/free/2018-03/large_watermarked/180228_B_04_preview.mp4' type="video/mp4"></source></video>
                <p>{contents}</p>
            </div>
        );
    }

}
