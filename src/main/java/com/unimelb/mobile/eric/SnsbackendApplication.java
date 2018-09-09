package com.unimelb.mobile.eric;

import com.unimelb.mobile.eric.controllers.HomeController;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan(basePackageClasses = HomeController.class)
public class SnsbackendApplication {

	public static void main(String[] args) {
		SpringApplication.run(SnsbackendApplication.class, args);
	}
}
