package com.function;

import java.util.*;
import com.microsoft.azure.functions.annotation.*;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with HTTP Trigger.
 */
public class OddOrEven {

    @FunctionName("HttpTrigger-Java")
    public HttpResponseMessage HttpTriggerJava(
            @HttpTrigger(name = "req", methods = {HttpMethod.GET, HttpMethod.POST}, authLevel = AuthorizationLevel.ANONYMOUS) HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        try {
            context.getLogger().info("Java HTTP trigger processed a request.");
        
            String numberQueryValue = request.getQueryParameters().get("number");
            int number = Integer.parseInt(numberQueryValue);

            return request
                .createResponseBuilder(HttpStatus.OK)
                .body(number % 2 == 0 ? "Even" : "Odd")
                .build();
        } 
        catch(NumberFormatException nfe) {
            return request
                .createResponseBuilder(HttpStatus.BAD_REQUEST)
                .body("Unable to parse")
                .build();
        }
    }
}
